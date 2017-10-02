
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace CoWorker.Models.Security.OAuth.Twitch
{
    internal class TwitchHandler : OAuthHandler<TwitchOptions>
    {
        public TwitchHandler(
            IOptionsMonitor<TwitchOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(
            ClaimsIdentity identity,
            AuthenticationProperties properties,
            OAuthTokenResponse tokens)
        {
            // Get the Twitch user
            using (var request = new HttpRequestMessage(
                HttpMethod.Get,
                Options.UserInformationEndpoint))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", tokens.AccessToken);

                var response = await Backchannel.SendAsync(request, Context.RequestAborted);
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(
                        $"An error occurred when retrieving user information ({response.StatusCode})." +
                        $" Please check if the authentication information is correct " +
                        $"and the corresponding Twitch API is enabled.");

                var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, properties, Scheme.Name);
                var context = new OAuthCreatingTicketContext(
                    principal,
                    properties,
                    Context,
                    Scheme,
                    Options,
                    Backchannel,
                    tokens,
                    payload);
                context.RunClaimActions();

                await Events.CreatingTicket(context);

                return context.Result.Ticket;
            }
        }
        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var queryStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            queryStrings.Add("response_type", "code");
            queryStrings.Add("client_id", Options.ClientId);
            queryStrings.Add("redirect_uri", redirectUri);

            AddQueryString(queryStrings, properties, "scope", FormatScope());

            var state = Options.StateDataFormat.Protect(properties);
            queryStrings.Add("state", state);

            return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryStrings);
        }

        private static void AddQueryString(
            IDictionary<string, string> queryStrings,
            AuthenticationProperties properties,
            string name,
            string defaultValue = null)
        {
            string value;
            if (!properties.Items.TryGetValue(name, out value))
            {
                value = defaultValue;
            }
            else
            {
                // Remove the parameter from AuthenticationProperties so it won't be serialized to state parameter
                properties.Items.Remove(name);
            }

            if (value == null)
            {
                return;
            }

            queryStrings[name] = value;
        }
        async protected override Task<OAuthTokenResponse> ExchangeCodeAsync(String code, String redirectUri)
        {

            base.Request.Query.TryGetValue("state", out var value);
            base.Request.Query.TryGetValue("scope", out var scope);
            var tokenRequestParameters = new Dictionary<string, string>()
            {
                ["client_id"] = Options.ClientId ,
                ["redirect_uri"] = redirectUri ,
                ["client_secret"] = Options.ClientSecret ,
                ["code"] = code ,
                ["grant_type"] = "authorization_code" ,
                ["state"] = value ,
                ["scope"] = scope,
            };
            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, Options.TokenEndpoint);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.twitchtv.v5+json"));
            requestMessage.Content = requestContent;
            var response = await Backchannel.SendAsync(requestMessage, Context.RequestAborted);
            if (response.IsSuccessStatusCode)
            {
                var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
                return OAuthTokenResponse.Success(payload);
            }
            else
            {
                var error = "OAuth token endpoint failure: " + await Display(response);
                return OAuthTokenResponse.Failed(new Exception(error));
            }
        }

        private static async Task<string> Display(HttpResponseMessage response)
        {
            var output = new StringBuilder();
            output.Append("Status: " + response.StatusCode.ToString() + ";");
            output.Append("Headers: " + response.Headers.ToString() + ";");
            output.Append("Body: " + await response.Content.ReadAsStringAsync() + ";");
            return output.ToString();
        }


    }
}
