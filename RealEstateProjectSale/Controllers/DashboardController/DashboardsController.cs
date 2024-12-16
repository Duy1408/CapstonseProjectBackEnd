using Microsoft.AspNetCore.Authorization;
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


        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("mothlytotalprice")]
        [SwaggerOperation(Summary = "Get monthly total prices")]
        public ActionResult<object> GetMonthlyTotalPrices()
        {
            var monthlyTotalPrices = _dashboardService.GetMonthlyTotalPrices();
            return Ok(monthlyTotalPrices);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("totalprice")]
        [SwaggerOperation(Summary = "total price")]
        public ActionResult<object> CalculateTotalPrice()
        {
            var totalprices = _dashboardService.CalculateTotalPrice();
            var formattedAmount = totalprices.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
            return Ok(formattedAmount);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("countproperty")]
        [SwaggerOperation(Summary = "count property")]
        public ActionResult<object> CountProperty()
        {
            var property = _dashboardService.CalculateProperty();
            var sumproperty = _dashboardService.SumProperty();
            return Ok(property + "/" + sumproperty);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("countcustomer")]
        [SwaggerOperation(Summary = "count customer")]
        public ActionResult<object> CountCustomer()
        {
            var customer = _dashboardService.CalculateCustomer();

            return Ok(customer);
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("outstanding-amount")]
        [SwaggerOperation(Summary = "outstanding amount")]
        public ActionResult<object> OutstandingAmount()
        {
            var outstandingamount = _dashboardService.CalculateOutstandingAmount();
            var formattedAmount = outstandingamount.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));
            return Ok(formattedAmount);
        }


    }


}
