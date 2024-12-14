using DvdShop.Database;
using DvdShop.DTOs;
using DvdShop.Entity;
using DvdShop.Interface.IServices;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Services
{
    public class ReportService: IReportService
    {
        private readonly DvdStoreContext _context;

        public ReportService(DvdStoreContext context)
        {
            _context = context;
        }

        public ReportsSummary GetReportsSummary(DateTime startDate, DateTime endDate)
        {
            // Customer Summary
            var customers = _context.Customers
                .Where(c => c.JoinDate >= startDate && c.JoinDate <= endDate)
                .ToList();
            var customerReports = customers.Select(c => new CustomerReport
            {
                CustomerId = c.Id,
                FullName = $"{c.FirstName} {c.LastName}",
                TotalRentals = c.Rentals.Count,
                TotalAmountSpent = c.Rentals.Sum(r => r.Copies * r.DVD.Price)
            }).ToList();

            // DVD Summary
            var dvds = _context.DVDs
                .Include(d => d.Rentals)
                .Where(d => d.Rentals.Any(r => r.RequestDate >= startDate && r.RequestDate <= endDate))
                .ToList();
            var dvdReports = dvds.Select(d => new DvdReport
            {
                DvdId = d.Id,
                Title = d.Title,
                TotalInventory = d.Inventory.AvailableCopies,
                RentedOut = d.Rentals.Count(r => r.Status == RentalStatus.Collected)
            }).ToList();

            // Rental Summary
            var rentals = _context.Rentals
                .Where(r => r.RequestDate >= startDate && r.RequestDate <= endDate)
                .ToList();
            var rentalSummary = new RentalSummaryReport
            {
                TotalRentals = rentals.Count,
                TotalRevenue = rentals.Sum(r => r.Copies * r.DVD.Price)
            };

            return new ReportsSummary
            {
                CustomerReports = customerReports,
                DvdReports = dvdReports,
                RentalSummary = rentalSummary
            };
        }
    }
}
