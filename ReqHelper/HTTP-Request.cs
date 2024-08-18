using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReqHelper
{
    public class HTTP_Request : IDisposable
    {
        private HttpClient _httpClient;
        private HttpRequestMessage _requestMessage;

        public HTTP_Request(string url)
        {
            _httpClient = new HttpClient();
            _requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        }

        /// <summary>
        /// Set the headers
        /// </summary>
        /// <param name="headers">List of headers</param>
        public void SetHeaders(List<HTTP_RequestHeader> headers)
        {
            if (_requestMessage == null) throw new InvalidOperationException("Request message is not initialized.");

            foreach (var header in headers)
            {
                if (header.Header != null && header.Value != null)
                {
                    _requestMessage.Headers.TryAddWithoutValidation(header.Header, header.Value);
                }
            }
        }

        /// <summary>
        /// Set the cookies
        /// </summary>
        /// <param name="cookies">List of cookies</param>
        public void SetCookies(List<HTTP_RequestCookie> cookies)
        {
            if (_requestMessage == null) throw new InvalidOperationException("Request message is not initialized.");

            var cookieContainer = new CookieContainer();
            var uri = _requestMessage.RequestUri ?? new Uri("http://localhost"); // Default URI if not set
            foreach (var cookie in cookies)
            {
                if (cookie.Cookie != null && cookie.Value != null)
                {
                    cookieContainer.Add(uri, new Cookie(cookie.Cookie, cookie.Value));
                }
            }

            var handler = new HttpClientHandler { CookieContainer = cookieContainer };
            _httpClient.Dispose(); // Dispose of the old client
            _httpClient = new HttpClient(handler);
        }

        /// <summary>
        /// Set the post parameters
        /// </summary>
        /// <param name="postParams">List of parameters</param>
        public void SetPost(List<HTTP_RequestParamaters> postParams)
        {
            if (_requestMessage == null) throw new InvalidOperationException("Request message is not initialized.");

            var content = new FormUrlEncodedContent(
                postParams.ConvertAll(param => new KeyValuePair<string, string>(param.Param, param.Value)));
            _requestMessage.Method = HttpMethod.Post;
            _requestMessage.Content = content;
        }

        /// <summary>
        /// Make a HTTP Request
        /// </summary>
        /// <param name="host">Host of Request</param>
        /// <param name="userAgent">UserAgent</param>
        /// <param name="contentType">ContentType</param>
        /// <param name="accept">Accept</param>
        /// <param name="referer">Referer</param>
        /// <param name="postParams">Post parameters</param>
        /// <param name="isPost">Is POST request</param>
        /// <returns>Source of Req</returns>
        public async Task<string> ExecuteAsync(
            string host,
            string userAgent,
            string contentType,
            string accept,
            string referer,
            List<HTTP_RequestParamaters> postParams,
            bool isPost)
        {
            if (_httpClient == null || _requestMessage == null)
                throw new InvalidOperationException("HTTP Client or Request message is not initialized.");

            try
            {
                _requestMessage.Headers.Host = host;
                _requestMessage.Headers.UserAgent.ParseAdd(userAgent);
                _requestMessage.Headers.Accept.ParseAdd(accept);
                _requestMessage.Headers.Referrer = new Uri(referer);

                if (isPost)
                {
                    SetPost(postParams);
                }

                HttpResponseMessage response = await _httpClient.SendAsync(_requestMessage);
                response.EnsureSuccessStatusCode(); // Throw if not success status code

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        /// <summary>
        /// Make a HTTP Request with Proxy
        /// </summary>
        /// <param name="host">Host of Request</param>
        /// <param name="userAgent">UserAgent</param>
        /// <param name="contentType">ContentType</param>
        /// <param name="accept">Accept</param>
        /// <param name="referer">Referer</param>
        /// <param name="proxy">Proxy</param>
        /// <param name="postParams">Post parameters</param>
        /// <param name="isPost">Is POST request</param>
        /// <returns>Source of Req</returns>
        public async Task<string> ExecuteAsync(
            string host,
            string userAgent,
            string contentType,
            string accept,
            string referer,
            WebProxy proxy,
            List<HTTP_RequestParamaters> postParams,
            bool isPost)
        {
            var handler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true
            };

            _httpClient?.Dispose(); // Dispose of the old client if it exists
            _httpClient = new HttpClient(handler);

            return await ExecuteAsync(host, userAgent, contentType, accept, referer, postParams, isPost);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _requestMessage?.Dispose();
        }
    }
}
