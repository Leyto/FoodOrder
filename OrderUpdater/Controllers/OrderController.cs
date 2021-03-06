using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using OrderUpdater.Service;
using System.Threading.Tasks;
using OrderUpdater;

namespace TestWebApi.Controllers
{
    /// <summary>
    ///  онтроллер по обработке призод€щих заказрв.
    /// </summary>
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly CustomContext _context;
        public OrderController(CustomContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("talabat")]
        public async Task<IActionResult> PostTalabatOrder([FromBody] JsonElement newOrder)
        {
            if (OrderHelper.BuildAndSaveOrder(_context, newOrder, "talabat"))
                return Ok("talabat");

            return BadRequest();
        }

        [HttpPost]
        [Route("zomato")]
        public async Task<IActionResult> PostZomatoOrder([FromBody] JsonElement newOrder)
        {
            if (OrderHelper.BuildAndSaveOrder(_context, newOrder, "zomato"))
                return Ok("zomato");

            return BadRequest();
        }

        [HttpPost]
        [Route("uber")]
        public async Task<IActionResult> PostUberOrder([FromBody] JsonElement newOrder)
        {
            if (OrderHelper.BuildAndSaveOrder(_context, newOrder, "uber"))
                return Ok("uber");

            return BadRequest();
        }
    }
}
