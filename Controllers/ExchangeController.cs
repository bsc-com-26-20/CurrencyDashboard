using Microsoft.AspNetCore.Mvc;
using CurrencyDashboard.Services;
using CurrencyDashboard.Models;
using CurrencyDashboard.Data; // your DbContext namespace
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CurrencyDashboard.Controllers
{
    public class ExchangeController : Controller
    {
        private readonly ExchangeRateService _exchangeService;
        private readonly AppDbContext _context;

        public ExchangeController(ExchangeRateService exchangeService, AppDbContext context)
        {
            _exchangeService = exchangeService;
            _context = context;
        }

        // Fetch and store latest rates in the database
        [HttpGet("api/exchange/fetch")]
        public async Task<IActionResult> FetchAndStoreRates()
        {
            // ✅ Base currency is now MWK
            var rates = await _exchangeService.GetLatestRatesAsync("MWK");

            if (rates == null || rates.Count == 0)
            {
                return BadRequest(new { Message = "No exchange rates were retrieved." });
            }

            _context.ExchangeRates.AddRange(rates);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Exchange rates updated successfully", Count = rates.Count });
        }

        // Dashboard view showing latest data
        [HttpGet]
        public IActionResult Dashboard()
        {
            var rates = _context.ExchangeRates
                .OrderByDescending(r => r.Timestamp)
                .Take(100)
                .ToList();

            var majorCurrencies = new[] { "USD", "EUR", "GBP", "ZAR", "KES" };

            // Extract most recent entry for each major currency
            ViewBag.MajorRates = rates
                .Where(r => majorCurrencies.Contains(r.TargetCurrency))
                .GroupBy(r => r.TargetCurrency)
                .Select(g => g.OrderByDescending(x => x.Timestamp).First())
                .ToList();

            return View(rates);
        }
    }
}
