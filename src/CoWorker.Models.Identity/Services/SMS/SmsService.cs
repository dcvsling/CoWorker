using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace IdentitySample.Controllers
{

    public class SmsService : ISmsSender
    {
        private readonly SMSoptions _options;
        public SmsService(IOptions<SMSoptions> options)
        {
            _options = options.Value;
        }

        public Task SendSmsAsync(string v, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
