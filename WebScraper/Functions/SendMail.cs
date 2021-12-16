using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebScraper.Functions
{
    public class SendMail
    {
        public static void Appointment(string appointment, string url)
        {
            var fromAddress = new MailAddress("doctorscraper2000@gmail.com", "Web Scraper");
            var toAddress = new MailAddress("r0843822@student.thomasmore.be", "My Name");
            string fromPassword = Password.Pass();
            const string subject = "Appointment was found!";
            string body = "An appointment has become available on: " + appointment + "\n" + url;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}