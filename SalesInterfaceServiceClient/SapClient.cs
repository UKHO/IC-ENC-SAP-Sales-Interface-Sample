using System;
using System.ServiceModel;

namespace SalesInterfaceServiceClient
{
    public class SapClient
    {
        //// change this to be url you wish to test against
        private const string Url = "sales_interface_service_environment_url_goes_here";

        // change this to your subscription key
        private const string SubscriptionKey = "your_subscription_key_goes_here";

        public z_sales_interfaceClient CreateSapClient()
        {
            var binding = CreateBasicHttpBinding();

            var endpoint = new EndpointAddress(
                new Uri(Url + "/?soapAction=https://engenavigator.engineering.ukho.gov.uk/1.0/IOrderingService/SubmitOrder&subscription-key=" + SubscriptionKey)
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
