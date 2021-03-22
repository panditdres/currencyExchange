using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchange.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace CurrencyExchange.Services
{
    public class ApiClient : IApiClient
    {
				private readonly HttpClient _httpClient;

				public ApiClient(HttpClient httpClient)
				{
						_httpClient = httpClient;
				}

				public async Task<T> GetJsonAsync<T>(IDictionary<string, string> queryParameters, string endPoint)
				{
						var response = await GetAsync(queryParameters, endPoint);
						if (response == null)
						{
								return default;
						}
						var stringContent = await response.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<T>(stringContent);
				}

				private async Task<HttpContent> GetAsync(IDictionary<string, string> queryParameters, string endPoint)
				{
						return await RestAsync(
								endPoint,
								async (x, y) => await x.GetAsync(y),
								queryParameters).ConfigureAwait(true);
				}

				private async Task<HttpContent> RestAsync(string endPoint, Func<HttpClient, string, Task<HttpResponseMessage>> restCall, IDictionary<string, string> queryParams)
				{
						HttpResponseMessage response = null;

						try
						{
								if (!string.IsNullOrWhiteSpace(endPoint))
								{
										var fullPath = QueryHelpers.AddQueryString(endPoint, queryParams);

										response = await restCall(_httpClient, fullPath).ConfigureAwait(true);

										if (response.Content == null || response.StatusCode == HttpStatusCode.NoContent)
										{
                        throw new HttpRequestException("Null response");
										}
										if (!response.IsSuccessStatusCode)
										{
                        throw new HttpRequestException($"Response code was: {response.StatusCode}");
										}
								}
						}
						catch (Exception e)
						{
                throw new HttpRequestException($"Exception while executing the call with issue {e}");
						}

						return response?.Content;
				}
		}
}
