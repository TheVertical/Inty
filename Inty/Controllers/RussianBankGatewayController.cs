using System;
using System.Threading;
using System.Threading.Tasks;
using Inty.Valutes.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inty.Controllers
{
    [ApiController]
    [Route("/api/russian-bank/gateway")]
    public sealed class RussianBankGatewayController : ControllerBase
    {
        private readonly IValuteCursInfoService _valuteCursInfoService;

        public RussianBankGatewayController(IValuteCursInfoService valuteCursInfoService)
        {
            _valuteCursInfoService = valuteCursInfoService;
        }

        /// <summary>
        /// Получить информацию о курсе валют
        /// </summary>
        /// <param name="dateTime">дата курса</param>
        /// <param name="codes">код валюты (CH)</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>список валют</returns>
        [HttpGet("curses")]
        public async Task<IActionResult> GetCurs([FromQuery] DateTime? dateTime, [FromQuery] string[] codes, CancellationToken cancellationToken = default)
        {
            var dtos = await _valuteCursInfoService.GetValuteCursInfo(dateTime, codes, cancellationToken);

            if (dtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(dtos);
        }
    }
}