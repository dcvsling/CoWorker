using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System;

namespace IdentitySample.Controllers
{

    public class EmailService : IEmailSender
    {
        private readonly IOptions<SmtpClient> _client;

        public EmailService(IOptions<SmtpClient> client)
        {
            _client = client;
        }

        async public Task SendAsync(Action<MailMessage> builder)
        {
            var msg = new MailMessage();
            builder(msg);
            await _client.Value.SendMailAsync(msg);
        }
    }
}
