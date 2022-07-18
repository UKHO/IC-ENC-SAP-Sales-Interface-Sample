using System;
using System.ServiceModel;

namespace SalesInterfaceServiceClient
{
    public class SapClient
    { 
        // change this to your subscription key
        private const string SubscriptionKey = "b5ec911e2a2d44b59c457f27a4eef142";

        // change this to be url you wish to test against
        private const string Url = "https://admiralty.azure-api.net/salesinterfaceservice-PREPROD";

        public z_sales_interfaceClient CreateSapClient()
        {
            var binding = CreateBasicHttpBinding();
            var endpoint = new EndpointAddress(
                new Uri(Url + "?subscription-key=" + SubscriptionKey)
            );

            var client = new z_sales_interfaceClient(binding, endpoint);

            return client;
        }

        private static BasicHttpBinding CreateBasicHttpBinding()
        {
            return new BasicHttpBinding
            {
                MaxReceivedMessageSize = int.MaxValue,
                Security = { Mode = BasicHttpSecurityMode.Transport }
            };
        }
    }
}
