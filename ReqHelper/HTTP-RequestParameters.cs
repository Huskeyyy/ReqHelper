using System;

namespace ReqHelper
{
    /// <summary>
    /// Represents an HTTP request parameter with its name and value.
    /// </summary>
    public class HTTP_RequestParamaters
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Param { get; }

        /// <summary>
        /// Gets the value of the parameter.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HTTP_RequestParamaters"/> class.
        /// </summary>
        /// <param name="param">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <exception cref="ArgumentException">Thrown when param or value is null or empty.</exception>
        public HTTP_RequestParamaters(string param, string value)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentException("Parameter name cannot be null or empty.", nameof(param));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Parameter value cannot be null or empty.", nameof(value));
            }

            Param = param;
            Value = value;
        }

        /// <summary>
        /// Returns a string representation of the HTTP_RequestParamaters object.
        /// </summary>
        /// <returns>A string that represents the current HTTP_RequestParamaters object.</returns>
        public override string ToString()
        {
            return $"{Param}={Value}";
        }
    }
}
