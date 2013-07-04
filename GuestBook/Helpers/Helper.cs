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
}