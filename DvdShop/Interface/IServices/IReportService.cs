using DvdShop.DTOs;

namespace DvdShop.Interface.IServices
{
    public interface IReportService
    {
        ReportsSummary GetReportsSummary(DateTime startDate, DateTime endDate);
    }
}
