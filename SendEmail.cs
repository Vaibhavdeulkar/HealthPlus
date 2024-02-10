using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Heathweb
{
    public class SendEmail
    {
      public static void SendEmails(string[] fromEmails, string subject, string HTMLbody)
        {
            try
            {
                var mail = new MailMessage();
                var client = new SmtpClient("mail.smtp2go.com", 2525) //Port 8025, 587 and 25 can also be used.
                {
                    Credentials = new NetworkCredential("vaibhavdeulkar12345", "cJTBt5hCjIvo9NZF"),
                    EnableSsl = true
                };
                mail.From = new MailAddress("vaibhavd@alohatechnologydev.com");
                foreach (var fromEmail in fromEmails)
                {
                    mail.To.Add(fromEmail);
                }
                mail.Subject = subject;
                var htmlBody = HTMLbody;
                var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                mail.AlternateViews.Add(htmlView);
                client.Send(mail);
                Console.WriteLine("Sent");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}