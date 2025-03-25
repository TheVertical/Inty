namespace Inty.Currencies.Dtos
{
    public class ExchangeRateInfoDto
    {
        public string Name { get; set; } = null!;

        public int Nominal { get; set; }

        public decimal Curs { get; set; }

        public int Code { get; set; }

        public string ChCode { get; set; } = null!;

        public decimal UnitRate { get; set; }
    }
}