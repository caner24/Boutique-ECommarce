using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Drawing;
using System.Text;

namespace Boutique.MailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(Encoding.UTF8, null,x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
