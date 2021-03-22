using System.Threading.Tasks;
using CurrencyExchange.Models;

namespace CurrencyExchange.Interfaces
{
    public interface IExchangeConverterService
    {
        Task<ConvertedCurrencyResponse> GetConvertedCurrencyAsync(CurrencyRequest request);
    }
}
