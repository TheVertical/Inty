using System;
using System.Threading;
using System.Threading.Tasks;
using Inty.Currencies.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inty.Currencies
{
    [ApiController]
    [Route("/api/currencies")]
    public sealed class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyExchangeRateInfoService _currencyExchangeRateInfoService;

        public CurrenciesController(ICurrencyExchangeRateInfoService currencyExchangeRateInfoService)
        {
            _currencyExchangeRateInfoService = currencyExchangeRateInfoService;
        }

        /// <summary>
        /// Получить информацию об обменном курсе валют из Банка Росии
        /// </summary>
        /// <param name="dateTime">дата курса</param>
        /// <param name="codes">код валюты (CH)</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>список валют</returns>
        [HttpGet("exchange-rate")]
        public async Task<IActionResult> GetExchangeRate([FromQuery] DateTime? dateTime, [FromQuery] string[] codes, CancellationToken cancellationToken = default)
        {
            var dtos = await _currencyExchangeRateInfoService.GetEchangeRateInfo(dateTime, codes, cancellationToken);

            if (dtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(dtos);
        }
    }
}