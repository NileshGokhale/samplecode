using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace GuestBook.Helpers
{
    public static class Helper
    {
        public static bool SendEmail(string from, string to, string subject, string messageBody, bool isHtml = true)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(from));
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(to));
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(subject));
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(messageBody));
            try
            {
                MailMessage message = new MailMessage(from, to, subject, messageBody);
                message.IsBodyHtml = isHtml;
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static string GetWelcomeEmailLink(string userName)
        {
            var base64string = ToBase64(userName + "|" + DateTime.Now.ToLongDateString());
            var link = GetValue("ValidateEmailLink");

            var retval = string.Format(link, HttpContext.Current.Request.Url.Port, base64string);
            return retval;
        }

        public static string ToBase64(string value)
        {
            byte[] array = System.Text.Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(array);
        }

        public static string FromBase64(string value)
        {
            byte[] buff = Convert.FromBase64String(value);
            var retval = System.Text.Encoding.UTF8.GetString(buff);
            return retval;
        }

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }

    /// <summary>
    /// Session helper class to manipulate session values
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// The ajax request session variable
        /// </summary>
        private const string AjaxRequestSessionVariable = "AjaxRequestData";
        /// <summary>
        /// Gets a value indicating whether this instance has ajax request.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has ajax request; otherwise, <c>false</c>.
        /// </value>
        public static bool HasAjaxRequest { get { return string.IsNullOrEmpty(AjaxRequest); } }
        /// <summary>
        /// Gets or sets the ajax request.
        /// </summary>
        /// <value>
        /// The ajax request.
        /// </value>
        public static string AjaxRequest
        {
            get
            {
                if (HttpContext.Current.Session[AjaxRequestSessionVariable] != null)
                {
                    return HttpContext.Current.Session[AjaxRequestSessionVariable].ToString();
                }
                return string.Empty;
            }
            set { HttpContext.Current.Session[AjaxRequestSessionVariable] = value; }
        }
    }
}