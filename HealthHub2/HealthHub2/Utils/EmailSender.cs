using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HealthHub2.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("yyy144197@gmail.com", "HealthHub");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}