using System.ComponentModel.DataAnnotations;
using CurrencyExchange.Validator;

namespace CurrencyExchange.Models
{
    public class CurrencyRequest
    {
        [Required]
        [StringLength(3)]
        [CurrencyValidator]
        public string SourceCurrency { get; set; }

        [Required]
        [StringLength(3)]
        [CurrencyValidator]
        public string TargetCurrency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
