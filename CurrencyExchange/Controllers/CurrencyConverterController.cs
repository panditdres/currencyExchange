using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyExchange.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly IExchangeConverterService _exchangeConverterService;

        public CurrencyConverterController(
            IExchangeConverterService exchangeConverterService, 
            ILogger<CurrencyConverterController> logger)
        {
            _exchangeConverterService = exchangeConverterService;
        }

        [HttpPost]
        [Route("ConvertCurrency")]
        public async Task<IActionResult> ConvertCurrencyAsync([FromBody] CurrencyRequest request) 
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                throw new HttpRequestException("Invalid request");
            }

            var response = await _exchangeConverterService.GetConvertedCurrencyAsync(request);

            return Ok(response);
        }
    }
}
