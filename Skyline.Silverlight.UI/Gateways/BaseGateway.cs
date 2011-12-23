using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;


namespace Skyline.Silverlight.UI.Gateways
{
    public class BaseGateway
    {
        protected EndpointAddress _serviceEndPoint = null;
        protected CustomBinding _binding = null;

        public BaseGateway(string serviceUrl)
        {
            var elements = new List<BindingElement>();

            BinaryMessageEncodingBindingElement binaryEncodingElement = new BinaryMessageEncodingBindingElement();
            elements.Add(binaryEncodingElement);

            if (BaseAppContext.IsSiteUsingSSL)
            {
                HttpsTransportBindingElement httpsTransportElement = new HttpsTransportBindingElement();
                httpsTransportElement.MaxBufferSize = 2147483600;
                httpsTransportElement.MaxReceivedMessageSize = 2147483600;            
                elements.Add(httpsTransportElement);
            }
            else
            {
                HttpTransportBindingElement httpTransportElement = new HttpTransportBindingElement();
                httpTransportElement.MaxBufferSize = 2147483600;
                httpTransportElement.MaxReceivedMessageSize = 2147483600;
                elements.Add(httpTransportElement);
            }

            _binding = new CustomBinding(elements);
            _serviceEndPoint = new EndpointAddress(new Uri(serviceUrl));
        }
    }
}
