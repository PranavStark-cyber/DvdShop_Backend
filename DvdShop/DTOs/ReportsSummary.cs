namespace DvdShop.DTOs
{
    public class ReportsSummary
    {
        public List<CustomerReport> CustomerReports { get; set; }
        public List<DvdReport> DvdReports { get; set; }
        public RentalSummaryReport RentalSummary { get; set; }
    }
}
