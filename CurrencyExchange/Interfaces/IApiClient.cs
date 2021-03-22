using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Interfaces
{
    public interface IApiClient
    {
        Task<T> GetJsonAsync<T>(IDictionary<string, string> queryParameters, string endPoint);
    }
}
