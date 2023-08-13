using BigBang.Order.Application.ApplicationCommands.CreateOrder;
using BigBang.Order.Application.ApplicationQueries.GetOrderByTrackingNumber;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BigBang.Order.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseController
    {
        [HttpGet("{trackingNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string trackingNumber)
        {
            var query = new GetOrderByTrackingNumberQuery() { TrackingNumber = trackingNumber };
            var response = await this.QueryDispatcher.QueryAsync(query);

            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var trackingNumber = command.GenerateTrackingNumber();

            await CommandDispatcher.SendAsync(command);
            return CreatedAtAction(nameof(Create), new { }, trackingNumber);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return  Ok();
        }

        [HttpPut]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }

    }
}
