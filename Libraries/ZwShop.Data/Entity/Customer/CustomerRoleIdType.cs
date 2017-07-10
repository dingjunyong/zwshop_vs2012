using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwShop.Data.Entity.CustomerManagement
{
    public enum CustomerRoleIdType:int
    {
        /// <summary>
        /// 游客
        /// </summary>
        Guest=0,

        /// <summary>
        /// 注册用户
        /// </summary>
        Registered=1,

        /// <summary>
        /// 管理员
        /// </summary>
        Administrator=999
    }
}
