using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MimeKit;

namespace Application.Services.EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public List<MailboxAddress> From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection? attachments)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
        public Message(string from, string subject, string content, IFormFileCollection? attachments)
        {
            From = new List<MailboxAddress>();
            From.Add(new MailboxAddress(from));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }


    }
}