
using System;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using CoWorker.Models.Security.OAuth.Twitch;

namespace CoWorker.Models.Security.OAuth
{
    public static class OAuthHelper
    {
        public static string GetEmail(JObject user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return TryGetFirstValue(user, "emails", "value");
        }

        private static string TryGetFirstValue(JObject user, string propertyName, string subProperty)
        {
            JToken value;
            if (user.TryGetValue(propertyName, out value))
            {
                var array = JArray.Parse(value.ToString());
                if (array != null && array.Count > 0)
                {
                    var subObject = JObject.Parse(array.First.ToString());
                    if (subObject != null)
                    {
                        if (subObject.TryGetValue(subProperty, out value))
                        {
                            return value.ToString();
                        }
                    }
                }
            }
            return null;
        }

    }
}
