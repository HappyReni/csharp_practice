using System.Net.Mail;
using System.Net;

namespace SportsResultNotifier
{
    public class Email
    {
        string smtpAddress = "smtp.gmail.com";
        int portNumber = 587;
        bool enableSSL = true;
        string emailFromAddress = "dnjsvlfwo@gmail.com"; //Sender Email Address  
        string password = "vjlc wibp cezc gohy"; //Sender Password  
        string emailToAddress = "dnjsvlfwo@gmail.com"; //Receiver Email Address  
        string subject = "Hello";
        string body = "Hello, This is Email sending test using gmail.";

        //public static void SendEmail()
        //{
        //    using (MailMessage mail = new MailMessage())
        //    {
        //        mail.From = new MailAddress(emailFromAddress);
        //        mail.To.Add(emailToAddress);
        //        mail.Subject = subject;
        //        mail.Body = body;
        //        mail.IsBodyHtml = true;
        //        //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
        //        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
        //        {
        //            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
        //            smtp.EnableSsl = enableSSL;
        //            smtp.Send(mail);
        //        }
        //    }
        //}
    }
}
