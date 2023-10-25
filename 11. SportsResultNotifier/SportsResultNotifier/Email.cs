using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace SportsResultNotifier
{
    public class EmailProperty
    {
        public string smtpAddress;
        public int portNumber;
        public bool enableSSL = true;
        public string emailFromAddress = "dnjsvlfwo@gmail.com"; //Sender Email Address  
        public string password = "vjlc wibp cezc gohy"; //Sender Password  
        public string emailToAddress = "dnjsvlfwo@gmail.com"; //Receiver Email Address  
        private readonly IConfiguration configuration;
        public EmailProperty()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var configs = configuration.GetChildren();
            foreach (var config in configs)
            {
                Console.WriteLine(config.Key + config.Value);
            }
        }
    }
    public class Email
    {
        private EmailProperty Prop { get; set; }

        public Email()
        {
            Prop = new();
        }
        
        public void Send()
        {
            //string subject = "Hello";
            //string body = "<h1>Hello, This is Email sending test using gmail.</h1>";

            //using (MailMessage mail = new MailMessage())
            //{
            //    mail.From = new MailAddress(emailFromAddress);
            //    mail.To.Add(emailToAddress);
            //    mail.Subject = subject;
            //    mail.Body = body;
            //    mail.IsBodyHtml = true;
            //    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
            //    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
            //    {
            //        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
            //        smtp.EnableSsl = enableSSL;
            //        smtp.Send(mail);
            //    }
            //}
            Console.Write("sent");

        }
    }
}
