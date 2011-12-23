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
using System.ServiceModel;

namespace Skyline.Silverlight.UI.Gateways
{
    public class BaseBasicHTTPGateway
    {
        protected EndpointAddress _serviceEndPoint = null;
        protected BasicHttpBinding _binding = null;


        public BaseBasicHTTPGateway(string serviceUrl)
        {
            _binding = new BasicHttpBinding();
            _binding.MaxBufferSize = 2147483600;
            _binding.MaxReceivedMessageSize = 2147483600;

            if (BaseAppContext.IsSiteUsingSSL)
            {
                _binding.Security.Mode = BasicHttpSecurityMode.Transport;
            }
            else
            {
                _binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            }

            _serviceEndPoint = new EndpointAddress(new Uri(serviceUrl));
        }
    }
}
