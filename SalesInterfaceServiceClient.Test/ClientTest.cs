using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SalesInterfaceServiceClient.Test
{
    [TestClass]
    public class ClientTest
    {
        // change this to your subscription key
        private readonly string _subscriptionKey = "b5ec911e2a2d44b59c457f27a4eef142";
        // change this to be url you wish to test against
        private readonly string _url = "https://admiralty.azure-api.net/salesinterfaceservice-PREPROD";

        [TestMethod]
        public void TestMethod1()
        {
            using (var client = CreateSapClient(_subscriptionKey, _url))
            {
                CheckZSalesInterface(client);
            }
        }

        private z_sales_interfaceClient CreateSapClient(string subscriptionKey, string url)
        {
            var binding = CreateBasicHttpBinding();
            var endpoint = new EndpointAddress(
                new Uri(_url + "?subscription-key=" + subscriptionKey)
            );

            var client = new z_sales_interfaceClient(binding, endpoint);

            return client;
        }

        /// <summary>
        /// Creates the basic HTTP binding.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private static BasicHttpBinding CreateBasicHttpBinding()
        {
            return new BasicHttpBinding
            {
                MaxReceivedMessageSize = int.MaxValue, Security = {Mode = BasicHttpSecurityMode.Transport}
            };
        }

        private static void CheckZSalesInterface(z_sales_interfaceClient client)
        {
            var result = client.Z_SALES_INTERFACE(new Z_SALES_INTERFACE
            {
                IM_ORDER = new ZSALES_ORDER()
                {
                    GUID = Guid.NewGuid().ToString(),
                    SERVICETYPE = "PAYS",
                    REPTYPE = "ORDER",
                    DOCTYPE = "ZPAY",
                    ACC = "21222",
                    UNIQORDID = Guid.NewGuid().ToString(),
                    SENDEMAIL = "sampleuser@someplace.com",
                    STARTDATE = DateTime.UtcNow.ToString("yyyy.MM.dd"),
                    ENDDATE = DateTime.UtcNow.ToString("yyyy.MM.dd"),

                    LIC = new[]
                    {
                        new ZSALES_LICENCEE
                        {
                            LNUM = "My Navigation",
                            CITY = "London",
                            VESSELS = new[]
                            {
                                new ZSALES_VESSELS
                                {
                                    VID = "12345",
                                    VNAME = "Pequod",
                                    IMO = "",
                                    CALLSIGN = "AHAB",
                                    ENDUSERID = "ENDUSERID1",
                                    ECDIS = new[]
                                    {
                                        new ZSALES_ECDIS
                                        {
                                            ID = "1234",
                                            MODEL = "White Whale 2000"
                                        }
                                    },
                                    SER = new ZSALES_SERVICE
                                    {
                                        SID = "90",
                                        LTYPE = "10",
                                        PO = "Pay0123456789",
                                        PROD = new[]
                                        {
                                            new ZSALES_PRODUCT
                                            {
                                                ID = "IN132KVM",
                                                DUR = "3",
                                                DUR_UNIT = "Month",
                                                QTY = "1",
                                                BEGDA = DateTime.UtcNow.AddMonths(-3).ToString("yyyy.MM.dd"),
                                                ENDDA = DateTime.UtcNow.ToString("yyyy.MM.dd")
                                            }
                                        }
                                    }
                                },
                            }
                        }
                    },
                }
            });

            Assert.IsNotNull(result.EX_STATUS);
            Assert.IsNotNull(result.EX_MESSAGE, result.EX_STATUS);
            Assert.AreNotEqual("", result.EX_STATUS);
            Assert.AreEqual("0", result.EX_STATUS);
            StringAssert.Contains(result.EX_MESSAGE, "Thank you for your order");
        }
    }
}

