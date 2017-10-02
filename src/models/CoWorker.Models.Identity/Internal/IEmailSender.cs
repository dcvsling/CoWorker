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

    public interface IEmailSender
    {
        Task SendEmailAsync(System.String v1, System.String v2, System.String message);
    }
}
