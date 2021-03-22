using System.Collections.Generic;

namespace CurrencyExchange.Models
{
    public class RatesResponse
    {
        public Dictionary<string,decimal> Rates { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
    }
}
