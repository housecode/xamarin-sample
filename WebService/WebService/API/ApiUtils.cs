using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebService.API {
	/**
	 * Created by Cahyo Agung
	 * @link      http://www.housecode.net
	 * @copyright Copyright (c) 2017 Housecode
	 * @version   1.0.0
	 * 
	 * This Library need following packages
	 * - Microsoft.net.http
	 * - Newtonsoft.Json
	 * - System.IO.Comppresion
	 */
	public static class ApiUtils {
        /// <summary>
        /// Checks the URL exists.
        /// </summary>
        /// <returns>The URL exists.</returns>
        /// <param name="url">URL.</param>
		public static Task<bool> CheckUrlExists(string url) {
			return CheckInternet(url);
		}

        /// <summary>
        /// Checks the connection.
        /// </summary>
        /// <returns>The connection.</returns>
		public static Task<bool> CheckConnection() {
			return CheckInternet("");
		}

		private static Task<bool> CheckInternet(string url) {
            return Task.Run(async () => {
				try {
					if (string.IsNullOrWhiteSpace(url)) {
						return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
					}
					var client = new HttpClient(new HttpClientHandler { }) {
						Timeout = TimeSpan.FromMilliseconds(5000)
					};
					var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
					response.EnsureSuccessStatusCode();
					return true;
				} catch (Exception) {
                    return false;
				}
            });
		}

		private static Task<T> DeserializeObject<T>(Stream stream, CompressionMethod method) {
			return Task.Run(async () => {
				var json = "";
				StreamReader reader;
				switch (method) {
					case CompressionMethod.GZip:
					using (var decompres = new GZipStream(stream, CompressionMode.Decompress)) {
						reader = new StreamReader(decompres);
						json = await reader.ReadToEndAsync();
					}
					break;
					case CompressionMethod.Deflate:
					using (var decompres = new DeflateStream(stream, CompressionMode.Decompress)) {
						reader = new StreamReader(decompres);
						json = await reader.ReadToEndAsync();
					}
					break;
					default:
					reader = new StreamReader(stream);
					json = await reader.ReadToEndAsync();
					break;
				}
				var data = JsonConvert.DeserializeObject<T>(json);
				return data;
			});
		}

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="stream">Stream.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
		public static Task<T> DeserializeObject<T>(Stream stream) {
			return DeserializeObject<T>(stream, CompressionMethod.None);
		}

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="response">Response.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
		public static Task<T> DeserializeObject<T>(HttpResponseMessage response) {
			return Task.Run(async () => {
                var encode = Coalesce(response.Content.Headers.ContentEncoding, new List<string> { });
				var mode = encode.Count > 0 ? encode.ToString().ToLower() : "";
				CompressionMethod compres = CompressionMethod.None;
				if (mode.Contains("gzip")) {
					compres = CompressionMethod.GZip;
				} else if (mode.Contains("deflate")) {
					compres = CompressionMethod.Deflate;
				} else {
					compres = CompressionMethod.None;
				}
				return await DeserializeObject<T>(await response.Content.ReadAsStreamAsync(), compres);
			});
		}

		/// <summary>
		/// Coalesce the specified source and value.
		/// </summary>
		/// <returns>The coalesce.</returns>
		/// <param name="source">Source.</param>
		/// <param name="value">Value.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Coalesce<T>(T source, T value) {
			if (EqualityComparer<T>.Default.Equals(source, default(T))) {
				throw new Exception("Exception: Value cannot be 'null'");
			}

			if (EqualityComparer<T>.Default.Equals(source, default(T))) {
				return value;
			}
			return source;
		}

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="obj">Object.</param>
		public static string SerializeObject(object obj) {
			return JsonConvert.SerializeObject(obj);
		}

		private static string FixedUrl(string url) {
			if (string.IsNullOrWhiteSpace(url)) return "";

			if (url.Contains("?")) {
				int x = url.IndexOf('?');
				if (url.Substring(x - 1, 1).Equals("/")) {
					var str1 = url.Substring(0, x - 1);
					var str2 = url.Substring(x, url.Length - x);
					url = str1 + str2;
				}
			}

			return url;
		}

		public static bool IsValidEmail(string strIn) {
			return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
		}

		enum CompressionMethod {
			GZip, Deflate, None
		}
	}
}
