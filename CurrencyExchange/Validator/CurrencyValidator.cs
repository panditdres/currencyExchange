using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CurrencyExchange.Validator
{
    public class CurrencyValidator : ValidationAttribute
    {
        private static string AcceptedCurrency = "CAD,HKD,ISK,PHP,DKK,HUF,CZK,GBP,RON,SEK,IDR,INR,BRL,RUB,HRK,JPY,THB,CHF,EUR,MYR,BGN,TRY,CNY,NOK,NZD,ZAR,USD,MXN,SGD,AUD,ILS,KRW,PLN,GBP";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currencyArray = AcceptedCurrency.Split(",");
            return currencyArray.Contains(value) ? 
                ValidationResult.Success : 
                new ValidationResult("Invalid currency");
        }
    }
}
