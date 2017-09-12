using System;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.WebApi.Controllers
{
    /// <summary>
    /// Home controller created to redirect all routes that are not refined to the swagger doc
    /// </summary>
    [Route("{*url}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Home : Controller
    {
        /// <summary>
        /// Main route to make the redirect to the doc
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}