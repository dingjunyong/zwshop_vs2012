using System;
using ZwShop.Services.Infrastructure;

namespace ZwShop.Services.Messages
{
    public partial class EmailAccount : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets an email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets an email display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets an email host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets an email port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets an email user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets an email password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value that controls whether the SmtpClient uses Secure Sockets Layer (SSL) to encrypt the connection
        /// </summary>
        public bool EnableSSL { get; set; }

        /// <summary>
        /// Gets or sets a value that controls whether the default system credentials of the application are sent with requests.
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        #endregion

        #region Custom properties

        /// <summary>
        /// Gets a friendly email account name
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (!String.IsNullOrEmpty(this.DisplayName))
                    return this.Email + " (" + this.DisplayName + ")";
                    return this.Email;
            }
        }

        /// <summary>
        /// Gets or a value indicating whether the email account is default one
        /// </summary>
        public bool IsDefault
        {
            get
            {
                var defaultEmailAccount = IoC.Resolve<IMessageService>().DefaultEmailAccount;
                return ((defaultEmailAccount != null && defaultEmailAccount.Id == this.Id));
            }
        }

        #endregion

    }
}
