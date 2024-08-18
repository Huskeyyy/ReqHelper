using System;

namespace ReqHelper
{
    /// <summary>
    /// Represents an HTTP header with its name and value.
    /// </summary>
    public class HTTP_RequestHeader
    {
        /// <summary>
        /// Gets the name of the header.
        /// </summary>
        public string Header { get; }

        /// <summary>
        /// Gets the value of the header.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTP_RequestHeader"/> class.
        /// </summary>
        /// <param name="header">The name of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <exception cref="ArgumentException">Thrown when header or value is null or empty.</exception>
        public HTTP_RequestHeader(string header, string value)
        {
            if (string.IsNullOrWhiteSpace(header))
            {
                throw new ArgumentException("Header name cannot be null or empty.", nameof(header));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Header value cannot be null or empty.", nameof(value));
            }

            Header = header;
            Value = value;
        }

        /// <summary>
        /// Returns a string representation of the HTTP_RequestHeader object.
        /// </summary>
        /// <returns>A string that represents the current HTTP_RequestHeader object.</returns>
        public override string ToString()
        {
            return $"{Header}: {Value}";
        }
    }
}
