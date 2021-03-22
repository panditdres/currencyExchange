using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;

namespace CurrencyExchange.Services
{
    public class RatesService : IRatesService
    {
        private readonly IApiClient _apiClient;
        public RatesService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<RatesResponse> GetRatesAsync(string sourceCurrency)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "base", sourceCurrency },
            };

            try
            {
                return await _apiClient.GetJsonAsync<RatesResponse>(queryParameters, "latest");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new HttpRequestException();
            }
        }
    }
}
