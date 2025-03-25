using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;

namespace Inty.RussianBank.Adapters
{
    internal sealed class RussianBankAdapter : IRussianBankAdapter
    {
        private readonly IOptions<RussianBankIntegrationOptions> _integrationOptions;

        public RussianBankAdapter(IOptions<RussianBankIntegrationOptions> integrationOptions)
        {
            _integrationOptions = integrationOptions;
        }

        public async Task<ValuteData?> GetCurrenciesExchangeRateOnDate(DateTime? dateTime)
        {
            ValuteData? data = null;
            var client = CreateClient();

            try
            {
                dateTime ??= DateTime.UtcNow;
                var result = await client.GetCursOnDateXMLAsync(dateTime.Value);
                data = Deserialize<ValuteData>(result);
            }
            finally
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client.Abort();
                }
                else
                {
                    client.Close();
                }
            }

            return data;
        }

        private DailyInfoSoapClient CreateClient()
        {
            var binding = new BasicHttpsBinding();
            var endpointAddress = new EndpointAddress(_integrationOptions.Value.DailyInfoUri);

            var client = new DailyInfoSoapClient(binding, endpointAddress);
            return client;
        }

        private static T Deserialize<T>(XmlNode node) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(ValuteData));
            using var reader = new XmlNodeReader(node);
            return (xmlSerializer.Deserialize(reader) as T)!;
        }
    }
}