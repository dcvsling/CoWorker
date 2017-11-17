using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Linq;
using System.Linq.Expressions;

namespace CoWorker.Models.IdentityServer.Crypto
{

    public static class RandomGeneratorExtensions
    {
        private const string AllowableCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";

        public static T GenerateString<T>(this Func<string,T> action,int length)
        {
            using(var random = RandomNumberGenerator.Create(AllowableCharacters))
            {
                var bytes = new byte[length];
                random.GetBytes(bytes);
                return action(new string(bytes.Select(x => AllowableCharacters[x % AllowableCharacters.Length]).ToArray()));
            }
        }
    }

}
