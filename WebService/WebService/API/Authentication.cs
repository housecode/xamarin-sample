namespace WebService.API {
	/**
	 * Created by Cahyo Agung
	 * @link      http://www.housecode.net
	 * @copyright Copyright (c) 2017 Housecode
	 * @version   1.0.0
	 */
	public class Authentication {
        /// <summary>
        /// Gets or sets the type of the auth.
        /// </summary>
        /// <value>The type of the auth.</value>
		public AuthType AuthType { get; set; } = AuthType.None;

		private string _authValue = "", _authKey = "Authorization";
        /// <summary>
        /// Gets the auth key.
        /// </summary>
        /// <value>The auth key.</value>
		public string AuthKey { get { return AuthType == AuthType.None ? "" : _authKey; } }

        /// <summary>
        /// Gets or sets the auth value.
        /// </summary>
        /// <value>The auth value.</value>
		public string AuthValue {
			get { return _authValue; }
			set {
				switch (AuthType) {
					default:
					_authValue = "";
					break;
					case AuthType.Basic:
					_authValue = "Basic " + value;
					break;
					case AuthType.Bearer:
					_authValue = "Bearer " + value;
					break;
					case AuthType.OAuth:
					_authValue = "OAuth " + value;
					break;
					case AuthType.OAuth2:
					_authValue = "OAuth2 " + value;
					break;
				}
			}
		}
    }
}
