using MercadolibreChallenge.API.Managers.Implementation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var result = new StatsManager().GetStats();

            return await Task.FromResult(new JsonResult(result));
        }
    }
}
