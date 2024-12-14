using DvdShop.DTOs;
using DvdShop.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // Combined Report
        [HttpGet("summary")]
        public ActionResult<ReportsSummary> GetReportsSummary(DateTime startDate, DateTime endDate)
        {
            var reportsSummary = _reportService.GetReportsSummary(startDate, endDate);
            return Ok(reportsSummary);
        }
    }
}
