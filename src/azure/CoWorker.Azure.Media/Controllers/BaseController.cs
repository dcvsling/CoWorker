using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace CoWorker.Azure.Media.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Controller]
    public class BaseController
    {
        [ControllerContext]
        public ControllerContext ControllerContext { get; set; }
        public HttpContext HttpContext => ControllerContext.HttpContext;
        [NonAction]
        public Task<IActionResult> Ok<T>(Func<T> func)
            => Ok(Task.Run(() => func()));
        [NonAction]
        async public Task<IActionResult> Ok<T>(Task<T> task)
            => new OkObjectResult(await task);
        [NonAction]
        public Task<IActionResult> Accept<T>(Func<T> func)
            => Accept(Task.Run(() => func()));
        [NonAction]
        async public Task<IActionResult> Accept<T>(Task<T> task)
            => new AcceptedResult(HttpContext.Request.Path.Value, await task);
        [NonAction]
        public Task<IActionResult> Created<T>(Func<T> func)
            => Created(Task.Run(() => func()));
        [NonAction]
        async public Task<IActionResult> Created<T>(Task<T> task)
            => new CreatedResult(HttpContext.Request.Path.Value,await task);
        [NonAction]
        public Task<IActionResult> BadRequest<T>(Func<T> func)
            => BadRequest(Task.Run(() => func()));
        [NonAction]
        async public Task<IActionResult> BadRequest<T>(Task<T> task)
            => new BadRequestObjectResult(await task);
        [NonAction]
        public Task<IActionResult> Redirect(Func<string> func)
            => Redirect(Task.Run(() => func()));
        [NonAction]
        async public Task<IActionResult> Redirect(Task<string> task)
            => new RedirectResult(await task);
        [NonAction]
        public Task<IActionResult> NoContent(Action action)
            => NoContent(Task.Run(action));
        [NonAction]
        public Task<IActionResult> NoContent(Task task)
            => Task.FromResult<IActionResult>(new NoContentResult());
    }
}
