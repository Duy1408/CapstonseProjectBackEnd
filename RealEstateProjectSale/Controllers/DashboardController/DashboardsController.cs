using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.DashboardController
{
    [Route("api/dashboards")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }



        [HttpGet("mothlytotalprice")]
        [SwaggerOperation(Summary = "Get monthly total prices")]

        public ActionResult<object> GetMonthlyTotalPrices()
        {
            var monthlyTotalPrices = _dashboardService.GetMonthlyTotalPrices();
            return Ok(monthlyTotalPrices);
        }

        [HttpGet("totalprice")]
        [SwaggerOperation(Summary = "Calculate total price")]
        public ActionResult<object> CalculateTotalPrice()
        {
            var totalprices = _dashboardService.CalculateTotalPrice();
            return Ok(totalprices);
        }

        [HttpGet("countproperty")]
        [SwaggerOperation(Summary = "count property")]
        public ActionResult<object> CountProperty()
        {
            var property = _dashboardService.CalculateProperty();
            var sumproperty = _dashboardService.SumProperty();
            return Ok(property +"/"+ sumproperty);
        }
    }


}
