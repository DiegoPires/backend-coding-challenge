using System;
using Microsoft.AspNetCore.Mvc;

namespace backend_coding_challenge.Controllers
{
    /// <summary>
    /// Just to redirect all routes to the swagger
    /// </summary>
    [Route("{*url}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Home : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}