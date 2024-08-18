using System;

namespace ReqHelper
{
    /// <summary>
    /// Represents an HTTP cookie with its name and value.
    /// </summary>
    public class HTTP_RequestCookie
    {
        /// <summary>
        /// Gets the name of the cookie.
        /// </summary>
        public string Cookie { get; }

        /// <summary>
        /// Gets the value of the cookie.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTP_RequestCookie"/> class.
        /// </summary>
        /// <param name="cookie">The name of the cookie.</param>
        /// <param name="value">The value of the cookie.</param>
        /// <exception cref="ArgumentException">Thrown when cookie or value is null or empty.</exception>
        public HTTP_RequestCookie(string cookie, string value)
        {
            if (string.IsNullOrWhiteSpace(cookie))
            {
                throw new ArgumentException("Cookie name cannot be null or empty.", nameof(cookie));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cookie value cannot be null or empty.", nameof(value));
            }

            Cookie = cookie;
            Value = value;
        }

        /// <summary>
        /// Returns a string representation of the HTTP_RequestCookie object.
        /// </summary>
        /// <returns>A string that represents the current HTTP_RequestCookie object.</returns>
        public override string ToString()
        {
            return $"{Cookie}={Value}";
        }
    }
}
