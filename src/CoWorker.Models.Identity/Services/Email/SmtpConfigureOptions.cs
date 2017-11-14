﻿using System.Collections;
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

    public class SmtpConfigureOptions : IPostConfigureOptions<SmtpClient>
    {
        private readonly IOptionsSnapshot<EmailOptions> _options;

        public SmtpConfigureOptions(IOptionsSnapshot<EmailOptions> options)
        {
            _options = options;
        }

        public void PostConfigure(string name, SmtpClient client)
        {
            var options = _options.Get(name);
            client.Host = options.Host;
            client.Port = options.Port;
            client.EnableSsl = options.EnableSSL;
            client.UseDefaultCredentials = options.EnableSSL & options.CredentialPath.Any();
            if(options.CredentialPath.Any() && File.Exists(options.CredentialPath))
                client.ClientCertificates.Add(new X509Certificate(File.ReadAllBytes(options.CredentialPath)));

        }
    }
}
