using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SqueletteImplantation
{
    public class Courriel
    {
        private MimeMessage message { get; set; }
        private SmtpClient smtpClient { get; set; }
        public Courriel()
        {
            message = new MimeMessage();
            smtpClient = new SmtpClient();
        }
        public void SetHTMLMessage(string msg)
        {
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = msg;
            message.Body = bodyBuilder.ToMessageBody();
        }
        public void setSender(string email, string name)
        {
            message.From.Add(new MailboxAddress(name, email));
        }
        public void setDestination(string email)
        {
            message.To.Add(new MailboxAddress("", email));
        }
        public void addDestination(string email)
        {
            message.To.Add(new MailboxAddress("", email));

        }
        public void setSubject(string subject)
        {
            message.Subject = subject;
        }

        public void sendMessage()
        {
            smtpClient.Connect("smtp.mailgun.org", 587, false);
            smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
            smtpClient.Authenticate("1eb@dinf.cll.qc.ca", "LB0rD6YYQR5nTzMA1EIr");
            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}
