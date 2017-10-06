using System.Net;

namespace Housecode.Net.API {
	/**
	 * Created by Cahyo Agung
	 * @link      http://www.housecode.net
	 * @copyright Copyright (c) 2017 Housecode
	 * @version   1.0.0
	 */
	public class ServiceConfig {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Housecode.Net.API.ServiceConfig"/> use auth.
        /// </summary>
        /// <value><c>true</c> if use auth; otherwise, <c>false</c>.</value>
		public bool UseAuth { get; set; } = false;

        /// <summary>
        /// Gets or sets the authentication.
        /// </summary>
        /// <value>The authentication.</value>
		public Authentication Authentication { get; set; } = null;

        /// <summary>
        /// Gets or sets the accept.
        /// </summary>
        /// <value>The accept.</value>
		public string Accept { get; set; } = "application/json";

        /// <summary>
        /// Gets or sets the accept charset.
        /// </summary>
        /// <value>The accept charset.</value>
		public string AcceptCharset { get; set; } = "UTF-8";

        /// <summary>
        /// Gets or sets the accept encoding.
        /// </summary>
        /// <value>The accept encoding.</value>
		public string AcceptEncoding { get; set; } = "gzip, deflate";

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Housecode.Net.API.ServiceConfig"/> allow redirect.
        /// </summary>
        /// <value><c>true</c> if allow redirect; otherwise, <c>false</c>.</value>
		public bool AllowRedirect { get; set; } = false;

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
		public string Connection { get; set; } = "close";

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
		public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:46.0) Gecko/20100101 Firefox/46.0";

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Housecode.Net.API.ServiceConfig"/> use cookie.
        /// </summary>
        /// <value><c>true</c> if use cookie; otherwise, <c>false</c>.</value>
		public bool UseCookie { get; set; } = false;

        /// <summary>
        /// Gets or sets the cookie.
        /// </summary>
        /// <value>The cookie.</value>
		public CookieContainer Cookie { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Housecode.Net.API.ServiceConfig"/> use proxy.
        /// </summary>
        /// <value><c>true</c> if use proxy; otherwise, <c>false</c>.</value>
		public bool UseProxy { get; set; } = false;

        /// <summary>
        /// Gets or sets the proxy.
        /// </summary>
        /// <value>The proxy.</value>
		public WebProxy Proxy { get; set; }
    }
}
