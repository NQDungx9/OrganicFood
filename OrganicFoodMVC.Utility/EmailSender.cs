using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using PostmarkDotNet;
using PostmarkDotNet.Legacy;
using PostmarkDotNet.Model;
using System;
using System.Collections.Specialized;
using System.IO;
using System.IO.Pipelines;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OrganicFoodMVC.Utility
{
    public class EmailSender : IEmailSender
    {

        private readonly EmailOptions emailOptions;

        public EmailSender(IOptions<EmailOptions> options)
        {
            emailOptions = options.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            // SENDINBLUE

            /*string serverMail = emailOptions.ServerMail, username = emailOptions.UsernameMail, pass = emailOptions.Password;
            int portMail = emailOptions.PortServerMail;

            SmtpClient mySmtpClient = new SmtpClient(serverMail, portMail);

            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
                System.Net.NetworkCredential(username, pass);
            mySmtpClient.Credentials = basicAuthenticationInfo;

            // add from,to mailaddresses
            MailAddress from = new MailAddress(emailOptions.UsernameMail, "Shop Organic Food");
            MailAddress to = new MailAddress(email);
            MailMessage myMail = new MailMessage(from, to);

            // add ReplyTo
            MailAddress replyTo = new MailAddress(emailOptions.UsernameMail, "Shop Organic Food");
            myMail.ReplyToList.Add(replyTo);

            // set subject and encoding
            myMail.Subject = subject;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = htmlMessage;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;

            return mySmtpClient.SendMailAsync(myMail);*/


            //POSTMARK
            // Send an email asynchronously:
            /* var message = new PostmarkMessage()
             {
                 To = email,
                 From = "trongthuong@trongthuong.tk",
                 TrackOpens = true,
                 Subject = subject,
                 HtmlBody = htmlMessage,
                 MessageStream = "register",
             };

             var client = new PostmarkClient("e36750de-fc24-434e-9c3e-6305bfe89f42");
             var sendResult = await client.SendMessageAsync(message);

             if (sendResult.Status == PostmarkStatus.Success)
             {
                 Console.WriteLine("Response was: " + sendResult.Message);
             }
             else
             {

             }*/


            //outlook
            MailMessage message = new MailMessage(new MailAddress(emailOptions.UsernameMail, "Shop Organic Food"), new MailAddress(email));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = htmlMessage;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(emailOptions.UsernameMail, emailOptions.Password);
            client.Host = emailOptions.ServerMail;
            client.EnableSsl = true;
            client.Port = emailOptions.PortServerMail;
            return client.SendMailAsync(message);
        }

    }
}
