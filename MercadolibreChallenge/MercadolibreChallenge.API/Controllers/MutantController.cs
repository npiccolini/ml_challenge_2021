using MercadolibreChallenge.API.Managers.Implementation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        [HttpPost]
        public async Task<StatusCodeResult> Post([FromBody] JObject value)
        {
            if (value == null || !value.TryGetValue("dna", out JToken jToken))
                return await Task.FromResult(new StatusCodeResult((int)HttpStatusCode.BadRequest));
            var array = jToken.ToObject<string[]>();
            var manager = new MutantManager();

            var result = manager.IsMutant(array);
            manager.SaveData(array, result);

            return await Task.FromResult(new StatusCodeResult(result ? (int)HttpStatusCode.OK : (int)HttpStatusCode.Forbidden));
        }
    }
}
