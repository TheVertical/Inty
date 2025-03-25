using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Inty.Currencies.Dtos;

namespace Inty.Currencies.Services
{
    public interface ICurrencyExchangeRateInfoService
    {
        /// <summary>
        /// Получить информацию о курсе валют
        /// </summary>
        /// <param name="dateTime">дата, на которую запрашивается информация</param>
        /// <param name="currenciesCodes">коды возвращаемых валют (CH)</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>курсы валют</returns>
        /// <remarks>возвращаемая коллекция может быть пустой</remarks>
        Task<IReadOnlyCollection<ExchangeRateInfoDto>> GetEchangeRateInfo(
            DateTime? dateTime,
            string[] currenciesCodes,
            CancellationToken cancellationToken = default
        );
    }
}