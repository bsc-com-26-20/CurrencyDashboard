using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyDashboard.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string BaseCurrency { get; set; } = string.Empty; // avoids null warning
        public string TargetCurrency { get; set; } = string.Empty; // avoids null warning
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // add this property
    }
}
