using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Smtp;

namespace WebScraper.Functions
{
    public class SendMail
    {
        public static void Appointment(string appointment, string url)
        {
            /*SmtpClient client = new SmtpClient()
            {
                Host = "smtp@gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "doctorscraper2000@gmail.com",
                    Password = Password.Pass()
                },
                PickupDirectoryLocation = @"C:\CSharp\demoScraper"
            };
            MailAddress from = new MailAddress("doctorScraper2000@scraped.com", "Scraped Appointment");
            MailAddress to = new MailAddress("r0843822@student.thomasmore.be");
            MailMessage message = new MailMessage()
            {
                From = from,
                Subject = "Appointment was found!",
                Body = "An appointment has become available on: " + appointment + "\n" + url
            };
            message.To.Add(to);


            client.Send(message);*/
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