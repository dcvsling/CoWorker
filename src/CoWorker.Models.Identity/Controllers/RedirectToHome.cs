using Microsoft.AspNetCore.Mvc;

namespace IdentitySample.Controllers
{
    public class RedirectToHome : RedirectResult
    {
        public RedirectToHome() : base("/")
        {
        }
    }
}
