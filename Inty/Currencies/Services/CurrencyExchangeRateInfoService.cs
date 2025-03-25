using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inty.Currencies.Dtos;
using Inty.RussianBank;
using Inty.RussianBank.Adapters;

namespace Inty.Currencies.Services
{
    internal sealed class CurrencyExchangeRateInfoService : ICurrencyExchangeRateInfoService
    {
        private readonly IRussianBankAdapter _russianBankAdapter;

        public CurrencyExchangeRateInfoService(IRussianBankAdapter russianBankAdapter)
        {
            _russianBankAdapter = russianBankAdapter;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<ExchangeRateInfoDto>> GetEchangeRateInfo(
            DateTime? dateTime,
            string[] currenciesCodes,
            CancellationToken cancellationToken = default
        )
        {
            dateTime ??= DateTime.UtcNow;
            var data = await _russianBankAdapter.GetCurrenciesExchangeRateOnDate(dateTime);

            if (data == null)
            {
                return Array.Empty<ExchangeRateInfoDto>();
            }

            var curses = data.Valutes.Select(Map).ToArray();

            currenciesCodes = currenciesCodes.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();
            if (currenciesCodes.Length == 0)
            {
                return curses;
            }

            currenciesCodes = currenciesCodes.Select(x => x.ToLower()).ToArray();

            return curses.Where(x => currenciesCodes.Contains(x.ChCode.ToLower(), StringComparer.Ordinal)).ToArray();
        }

        private static ExchangeRateInfoDto Map(ValuteCursOnDate curs)
        {
            return new ExchangeRateInfoDto
            {
                Name = curs.Vname.Trim(), Nominal = curs.Vnom, Curs = curs.Vcurs, Code = curs.Vcode, ChCode = curs.VchCode, UnitRate = curs.VunitRate
            };
        }
    }
}