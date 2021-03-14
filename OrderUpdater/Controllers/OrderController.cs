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
        [Route("{ordertype}")]
        public async Task<IActionResult> PostOrder(string ordertype, [FromBody] JsonElement newOrder)
        {
            if (OrderHelper.BuildAndSaveOrder(_context, newOrder, ordertype)) 
                return Ok("New order add to Db");

            return BadRequest();
        }
    }
}
