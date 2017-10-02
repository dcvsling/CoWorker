
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace CoWorker.Security.OAuth.Twitch
{
    public class TwitchOptions : OAuthOptions
    {
        public static string AuthenticationScheme = "Twitch";

        public TwitchOptions()
        {
            this.CallbackPath = "/signin-twitch";
            this.AuthorizationEndpoint = "https://api.twitch.tv/kraken/oauth2/authorize";
            this.TokenEndpoint = "https://api.twitch.tv/kraken/oauth2/token";
            this.UserInformationEndpoint = "https://api.twitch.tv/kraken";
            this.RootEndpoint = "https://api.twitch.tv/kraken";
            this.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            this.SaveTokens = true;
            this.Scope.Add("openid");
            this.Scope.Add("user_read");
            this.Scope.Add("channel_read");
        }

        public string RootEndpoint { get; set; }

    }
}