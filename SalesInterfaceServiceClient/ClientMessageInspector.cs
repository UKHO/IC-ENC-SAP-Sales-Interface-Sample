using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace SalesInterfaceServiceClient
{
    public class ClientMessageInspector: IClientMessageInspector, IEndpointBehavior
    {
        private readonly string _subscriptionKey;

        public ClientMessageInspector(string subscriptionKey)
        {
            _subscriptionKey = subscriptionKey;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessage;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out var httpRequestMessageObject))
            {
                (httpRequestMessageObject as HttpRequestMessageProperty).Headers["Ocp-Apim-Subscription-Key"] = _subscriptionKey;
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
                httpRequestMessage.Headers["Ocp-Apim-Subscription-Key"] = _subscriptionKey;

            }
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        { 
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }
    }
}
