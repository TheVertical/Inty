using System.ComponentModel.DataAnnotations;

namespace Inty.RussianBank
{
    internal sealed class RussianBankIntegrationOptions
    {
        [Required]
        [Url]
        public string DailyInfoUri { get; set; } = null!;
    }
}