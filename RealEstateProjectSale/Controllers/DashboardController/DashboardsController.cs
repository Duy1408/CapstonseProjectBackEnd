using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

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



        [HttpGet("/GetMonthlyTotalPrices")]
        public ActionResult<object> GetMonthlyTotalPrices()
        {
            var monthlyTotalPrices = _dashboardService.GetMonthlyTotalPrices();
            return Ok(monthlyTotalPrices);
        }
    }


}
