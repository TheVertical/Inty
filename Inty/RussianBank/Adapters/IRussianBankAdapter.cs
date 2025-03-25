using System;
using System.Threading.Tasks;

namespace Inty.RussianBank.Adapters
{
    public interface IRussianBankAdapter
    {
        Task<ValuteData?> GetCurceOnDate(DateTime? dateTime);
    }
}