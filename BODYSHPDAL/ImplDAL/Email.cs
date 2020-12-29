using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using BODYSHPBLL.ImplBLL;
using BODYSHPDAL;
using BODYSHPDAL.DbContext;

namespace BODYSHPDAL.ImplDAL
{
    class Email
    {
        public static string sendEmail(string subject, string FromMailAddress, MailAddressCollection toEmail, MailAddressCollection ccEmail, string AttachMentPath, string Body)
        {
            string SMTPEmailHost = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
            string SMTPusername = System.Configuration.ConfigurationSettings.AppSettings["SmtpUserName"];
            string SMTPpass = System.Configuration.ConfigurationSettings.AppSettings["SmtpPassword"];
            string AllowEmail = System.Configuration.ConfigurationSettings.AppSettings["AllowEmail"];
            //string frmEmail = UserDAL.GetEmail(FromMailAddress);
            if (AllowEmail == "true")
            {
                try
                {
                    SmtpClient smtp = new SmtpClient(SMTPEmailHost);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(SMTPusername, SMTPpass);
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(FromMailAddress);

                    foreach (var mail in toEmail)
                    {
                        message.To.Add(mail);
                    }
                    if (ccEmail != null)
                    {
                        foreach (var mail in ccEmail)
                        {
                            message.CC.Add(mail);
                        }
                    }
                    //message.Bcc.Add("ankur.verma@phoenixtech.consulting");
                    message.Bcc.Add("narayan.joshi@phoenixtech.consulting");

                    message.IsBodyHtml = true;
                    message.Subject = subject;
                    message.Body = Body;
                    
                    message.Attachments.Add(new Attachment(AttachMentPath));
                    smtp.Send(message);

                    return "Success: Notification sent";
                }
                catch (Exception Ex)
                {
               
                    return "Failed: Email ";
                }
            }
            else
            {
                return "Email not allowed";
            }
        }

        public static string sendEmail(string RequestedBy, string RequestAssignedTo, string RequestCreatedBy, string body, string subject)
        {
            try
            {

                //string frmEmail = UserDAL.GetEmail(RequestedBy);
                //string toEmail = UserDAL.GetEmail(RequestAssignedTo);
                //string ccEmail = UserDAL.GetEmail(RequestCreatedBy);
                string frmEmail = "narayanjoshi25cs@gmail.com";
                string toEmail = "narayan.joshi@phoenixtech.consulting";
                string ccEmail = "narayanjoshi@hotmail.com";


                MailAddressCollection m = new MailAddressCollection();
                
                m.Add(toEmail);

                MailAddressCollection cc = new MailAddressCollection();
                
                cc.Add(ccEmail);
                string str = sendEmail(subject, frmEmail, m, cc, subject, body);
                return str;
            }
            catch
            {
                return "Error";
            }
        }
    }
}
