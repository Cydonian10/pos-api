using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntoVenta.Database;

namespace PuntoVenta.Modules.Discount
{
    [Route("api/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        public DiscountController(DataContext context)
        {
        }

        //[HttpGet]
        //public async Task<ActionResult> getDiscountsByProduct()
        //{

        //}
    }
}
