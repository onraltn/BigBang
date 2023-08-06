using Microsoft.AspNetCore.Mvc;

namespace BigBang.Order.Api.Controllers
{
    [Route("orders")]
    public class OrderController : BaseController
    {
        [HttpGet]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            
            return Ok();
        }
    }
}
