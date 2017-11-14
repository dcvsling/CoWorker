using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.IO;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoWorker.Models.Identity;
using CoWorker.Models.Identity.Accounts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System;
using System.Security.Cryptography.X509Certificates;
using CoWorker.Primitives;
using System.Collections.Generic;

namespace IdentitySample.Controllers
{

    public interface IEmailSender
    {
        Task SendAsync(Action<MailMessage> msg);
    }
}
