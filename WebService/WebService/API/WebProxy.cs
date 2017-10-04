using System;
using System.Net;

namespace WebService.API {
    public class WebProxy : IWebProxy {
		private readonly Uri _proxyUri;

		public WebProxy(Uri proxyUri) {
			_proxyUri = proxyUri;
		}

		public ICredentials Credentials { get; set; }

		public Uri GetProxy(Uri destination) {
			return _proxyUri;
		}

		public bool IsBypassed(Uri destination) {
			return false;
		}
    }
}
