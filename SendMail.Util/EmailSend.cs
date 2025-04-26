﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Util
{
    public class EmailSend : IEmailSend
    {
        private readonly EmailSettings _emailSettings;
        public EmailSend(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage(); // aanmaken van een mail-object
            mail.To.Add(new MailAddress(email));
            mail.From = new
            MailAddress("jellegoemaere6@gmail.com"); // hier komt jullie Gmail-adres
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient(_emailSettings.MailServer))
                {
                    smtp.Port = _emailSettings.MailPort;
                    smtp.EnableSsl = true;
                    smtp.Credentials =
                    new NetworkCredential(_emailSettings.Sender,
                    _emailSettings.Password);
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
