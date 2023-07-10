using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.API.Controllers
{
    [ApiController]
    [Route("api/{Controller}")]
    public class FakeController:ControllerBase
    {


        [HttpGet]
        public ActionResult<String> Get()
        {

            return "Hi";        }
    }
}
