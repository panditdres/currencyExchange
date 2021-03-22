using System.Threading.Tasks;
using CurrencyExchange.Models;

namespace CurrencyExchange.Interfaces
{
    public interface IRatesService
    {
        Task<RatesResponse> GetRatesAsync(string sourceCurrency);
    }
}
