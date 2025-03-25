using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Inty.RussianBank;
using Inty.RussianBank.Adapters;

namespace Inty.Valutes.Services
{
    public class ValuteCursInfoDto
    {
        public string Name { get; set; } = null!;

        public int Nominal { get; set; }

        public decimal Curs { get; set; }

        public int Code { get; set; }

        public string ChCode { get; set; } = null!;

        public decimal UnitRate { get; set; }
    }

    public interface IValuteCursInfoService
    {
        /// <summary>
        /// Получить информацию о курсе валют
        /// </summary>
        /// <param name="dateTime">дата, на которую запрашивается информация</param>
        /// <param name="codes">коды возвращаемых валют</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>курсы валют</returns>
        /// <remarks>возвращаемая коллекция может быть пустой</remarks>
        Task<IReadOnlyCollection<ValuteCursInfoDto>> GetValuteCursInfo(DateTime? dateTime, string[] codes, CancellationToken cancellationToken = default);
    }

    internal sealed class ValuteCursInfoService : IValuteCursInfoService
    {
        private readonly IRussianBankAdapter _russianBankAdapter;

        public ValuteCursInfoService(IRussianBankAdapter russianBankAdapter)
        {
            _russianBankAdapter = russianBankAdapter;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<ValuteCursInfoDto>> GetValuteCursInfo(
            DateTime? dateTime,
            string[] codes,
            CancellationToken cancellationToken = default
        )
        {
            dateTime ??= DateTime.UtcNow;
            var data = await _russianBankAdapter.GetCurceOnDate(dateTime);

            if (data == null)
            {
                return Array.Empty<ValuteCursInfoDto>();
            }

            var curses = data.Valutes.Select(Map).ToArray();

            codes = codes.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();
            if (codes.Length == 0)
            {
                return curses;
            }

            codes = codes.Select(x => x.ToLower()).ToArray();

            return curses.Where(x => codes.Contains(x.ChCode.ToLower(), StringComparer.Ordinal)).ToArray();
        }

        private static ValuteCursInfoDto Map(ValuteCursOnDate curs)
        {
            return new ValuteCursInfoDto
            {
                Name = curs.Vname.Trim(), Nominal = curs.Vnom, Curs = curs.Vcurs, Code = curs.Vcode, ChCode = curs.VchCode, UnitRate = curs.VunitRate
            };
        }
    }
}