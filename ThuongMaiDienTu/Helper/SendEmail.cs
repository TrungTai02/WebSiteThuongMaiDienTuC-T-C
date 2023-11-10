using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace ThuongMaiDienTu.Helper
{
    public  class SendEmail
    {
        #region Xác Thực mail
        public static bool SendEmails(string to, string subject, string body, string attachfile)
        {
            ConstantHelper constantHelper = new ConstantHelper();
            string emailSender = constantHelper.emailSender;
            string hostEmail = constantHelper.hostEmail;
            int port = constantHelper.PortEmail;
            string passwordSender = constantHelper.passwordSender;

            try
            {
               
                MailMessage msg = new MailMessage(emailSender, to, subject, body);
                using (var client = new SmtpClient(hostEmail, port))
                {
                    client.EnableSsl = true;

                    if(!string.IsNullOrEmpty(attachfile))
                    {
                        Attachment attachment = new Attachment(attachfile);
                        msg.Attachments.Add(attachment);
                    }    

                    NetworkCredential credential = new NetworkCredential(emailSender, passwordSender);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credential;
                    client.Send(msg);
                }


            }
            catch(Exception)
            {

                return false;

            }

            return true;
        }
        #endregion
    }
}