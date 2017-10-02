using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoWorker.Models.Identity;
using CoWorker.Models.Identity.Accounts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentitySample.Controllers
{

    public interface ISmsSender
    {
        Task SendSmsAsync(System.String v, System.String message);
    }
}
