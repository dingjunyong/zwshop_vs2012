﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ZwShop.Services.Caching;
using ZwShop.Services.Configuration.Settings;
using ZwShop.Services.Infrastructure;
using ZwShop.Common;
using ZwShop.Common.Utils;
using ZwShop.Data.Entity.CustomerManagement;

namespace ZwShop.Services.Audit.UsersOnline
{
    /// <summary>
    /// Represents an online user service
    /// </summary>
    public partial class OnlineUserService : IOnlineUserService
    {
        #region Const

        private const string TRACKINGCOOKIENAME = "Shop.onlineusertracking";

        #endregion

        #region Fields

        private static readonly object s_lock = new object();

        /// <summary>
        /// Cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        #endregion
        
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public OnlineUserService()
        {
            this._cacheManager = new ShopStaticCache();
        }

        #endregion
        
        #region Utilities

        private Dictionary<Guid, OnlineUserInfo> GetAnonymousUserList()
        {
            string key = "Shop.OnlineUserList.Anonymous";
            Dictionary<Guid, OnlineUserInfo> obj2 = _cacheManager.Get(key) as Dictionary<Guid, OnlineUserInfo>;
            if (obj2 != null)
            {
                return obj2;
            }
            else
            {
                obj2 = new Dictionary<Guid, OnlineUserInfo>();
                _cacheManager.Add(key, obj2);
            }

            return obj2;
        }

        private Dictionary<Guid, OnlineUserInfo> GetRegisteredUserList()
        {
            string key = "Shop.OnlineUserList.Registered";
            Dictionary<Guid, OnlineUserInfo> obj2 = _cacheManager.Get(key) as Dictionary<Guid, OnlineUserInfo>;
            if (obj2 != null)
            {
                return obj2;
            }
            else
            {
                obj2 = new Dictionary<Guid, OnlineUserInfo>();
                _cacheManager.Add(key, obj2);
            }

            return obj2;
        }


        /// <summary>
        /// Purges expired users
        /// </summary>
        /// <param name="userList">User list</param>
        protected void PurgeUsers(Dictionary<Guid, OnlineUserInfo> userList)
        {
            if (!this.Enabled)
                return;

            if (userList == null)
                throw new ArgumentNullException("userList");

            int expMinutes = 20;

            lock (s_lock)
            {
                //user list
                List<Guid> usersToRemove = new List<Guid>();

                foreach (KeyValuePair<Guid, OnlineUserInfo> kvp in userList)
                {
                    if (kvp.Value.LastVisit.AddMinutes(expMinutes) < DateTime.UtcNow)
                        usersToRemove.Add(kvp.Key);
                }

                foreach (Guid guid in usersToRemove)
                {
                    userList.Remove(guid);
                }
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Tracks current user
        /// </summary>
        public void TrackCurrentUser()
        {
            try
            {
                if (!this.Enabled || 
                    HttpContext.Current == null)
                    return;

                lock (s_lock)
                {
                    //getting current user info (OnlineUserInfo)
                    OnlineUserInfo oui = null;

                    //user list
                    Dictionary<Guid, OnlineUserInfo> userList = null;
                    if (ShopContext.Current.User != null && ShopContext.Current.User.CustomerRoleIdType != CustomerRoleIdType.Guest)
                    {
                        //registered user
                        userList = GetRegisteredUserList();

                        //find user info
                        if (userList.ContainsKey(ShopContext.Current.User.CustomerGuid))
                        {
                            oui = userList[ShopContext.Current.User.CustomerGuid];
                        }

                        //create new user if existing one was not found
                        if (oui == null)
                        {
                            oui = new OnlineUserInfo();
                            oui.OnlineUserGuid = ShopContext.Current.User.CustomerGuid;
                            oui.CreatedOn = DateTime.UtcNow;
                            userList.Add(oui.OnlineUserGuid, oui);
                        }

                        //update other properties
                        oui.LastVisit = DateTime.UtcNow;
                        oui.LastPageVisited = CommonHelper.GetThisPageUrl(true);
                        oui.IPAddress = ShopContext.Current.UserHostAddress;
                        oui.AssociatedCustomerId = ShopContext.Current.User.Id;
                        HttpContext.Current.Response.Cookies.Remove(TRACKINGCOOKIENAME);
                    }
                    else
                    {
                        //guest
                        userList = GetAnonymousUserList();

                        //find user info
                        string cookieValue = string.Empty;
                        if ((HttpContext.Current.Request.Cookies[TRACKINGCOOKIENAME] != null) && (HttpContext.Current.Request.Cookies[TRACKINGCOOKIENAME].Value != null))
                        {
                            cookieValue = HttpContext.Current.Request.Cookies[TRACKINGCOOKIENAME].Value;
                        }
                        if (!string.IsNullOrEmpty(cookieValue))
                        {
                            Guid onlineUserGuid = Guid.Empty;
                            Guid.TryParse(cookieValue, out onlineUserGuid);
                            if (onlineUserGuid != Guid.Empty)
                            {
                                if (userList.ContainsKey(onlineUserGuid))
                                {
                                    oui = userList[onlineUserGuid];
                                }
                            }
                        }

                        //create new user if existing one was not found
                        if (oui == null)
                        {
                            oui = new OnlineUserInfo();
                            oui.OnlineUserGuid = Guid.NewGuid();
                            oui.CreatedOn = DateTime.UtcNow;
                            userList.Add(oui.OnlineUserGuid, oui);
                        }

                        //update other properties
                        oui.LastVisit = DateTime.UtcNow;
                        oui.LastPageVisited = CommonHelper.GetThisPageUrl(true);
                        oui.IPAddress = ShopContext.Current.UserHostAddress;
                        oui.AssociatedCustomerId = null;

                        //save new cookie
                        HttpCookie cookie = new HttpCookie(TRACKINGCOOKIENAME);
                        cookie.Value = oui.OnlineUserGuid.ToString();
                        cookie.Expires = DateTime.Now.AddHours(1);
                        HttpContext.Current.Response.Cookies.Remove(TRACKINGCOOKIENAME);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }

                    //maximum online customers
                    int currentVisitors = GetAnonymousUserList().Count() + GetRegisteredUserList().Count();
                    if (currentVisitors > this.MaximumOnlineCustomers)
                    {
                        this.MaximumOnlineCustomers = currentVisitors;
                    }
                }
            }
            catch (ShopException exc)
            {
                Debug.WriteLine(exc.ToString());
            }
        }

        /// <summary>
        /// Clears user list
        /// </summary>
        public void ClearUserList()
        {
            if (!this.Enabled)
                return;

            lock (s_lock)
            {
                Dictionary<Guid, OnlineUserInfo> userList1 = GetAnonymousUserList();
                Dictionary<Guid, OnlineUserInfo> userList2 = GetRegisteredUserList();
                userList1.Clear();
                userList2.Clear();
            }
        }

        /// <summary>
        /// Purges expired users
        /// </summary>
        public void PurgeUsers()
        {
            if (!this.Enabled)
                return;

            PurgeUsers(GetAnonymousUserList());
            PurgeUsers(GetRegisteredUserList());
        }

        /// <summary>
        /// Get online users (guest)
        /// </summary>
        /// <returns>Online user list</returns>
        public List<OnlineUserInfo> GetGuestList()
        {
            lock (s_lock)
            {
                //user list
                List<OnlineUserInfo> users = new List<OnlineUserInfo>();

                Dictionary<Guid, OnlineUserInfo> userList = GetAnonymousUserList();
                foreach (KeyValuePair<Guid, OnlineUserInfo> kvp in userList)
                {
                    users.Add(kvp.Value);
                }

                return users.OrderByDescending(oui => oui.LastVisit).ToList();
            }
        }

        /// <summary>
        /// Get online users (registered)
        /// </summary>
        /// <returns>Online user list</returns>
        public List<OnlineUserInfo> GetRegisteredUsersOnline()
        {
            lock (s_lock)
            {
                //user list
                List<OnlineUserInfo> users = new List<OnlineUserInfo>();

                Dictionary<Guid, OnlineUserInfo> userList = GetRegisteredUserList();
                foreach (KeyValuePair<Guid, OnlineUserInfo> kvp in userList)
                {
                    users.Add(kvp.Value);
                }

                return users.OrderByDescending(oui => oui.LastVisit).ToList();
            }
        }

        /// <summary>
        /// Get online users (guests and registered users)
        /// </summary>
        /// <returns>Online user list</returns>
        public List<OnlineUserInfo> GetAllUserList()
        {
            lock (s_lock)
            {
                //user list
                var allUsers = new List<OnlineUserInfo>();
                allUsers.AddRange(GetRegisteredUsersOnline());
                allUsers.AddRange(GetGuestList());

                return allUsers.OrderByDescending(oui => oui.LastVisit).ToList();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether tracking online users is enabled
        /// </summary>
        public bool Enabled
        {
            get
            {
                return IoC.Resolve<ISettingManager>().GetSettingValueBoolean("OnlineUserManager.Enabled", false);
            }
            set
            {
                IoC.Resolve<ISettingManager>().SetParam("OnlineUserManager.Enabled", value.ToString());
            }
        }
        
        /// <summary>
        /// Gets a maximum online customer number
        /// </summary>
        public int MaximumOnlineCustomers
        {
            get
            {
                return IoC.Resolve<ISettingManager>().GetSettingValueInteger("OnlineUserManager.MaximumOnlineCustomers", 0);
            }
            set
            {
                IoC.Resolve<ISettingManager>().SetParam("OnlineUserManager.MaximumOnlineCustomers", value.ToString());
            }
        }
        #endregion
    }
}