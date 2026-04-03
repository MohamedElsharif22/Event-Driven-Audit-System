namespace DH.EventDrivenAuditSystem.APIs.Responses
{
    /// <summary>
    /// Represents a standard response returned by an API, including an HTTP status code and an optional message
    /// describing the result.
    /// </summary>
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the ApiResponse class with the specified status code and an optional message.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to associate with the response.</param>
        /// <param name="message">An optional message describing the response. If null, a default message based on the status code is used.</param>
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource was not found",
                500 => "Internal server error occurred",
                _ => "An error occurred"
            };
        }
    }
    /// <summary>
    /// Represents a standard response returned by an API operation, including the result data and a success indicator.
    /// </summary>
    /// <typeparam name="T">The type of the data returned by the API operation.</typeparam>
    /// <param name="data">The data associated with the API response. This value is typically the result of the operation.</param>
    /// <param name="success">A value indicating whether the API operation was successful. The default is <see langword="true"/>.</param>
    public class ApiResponse<T>(T data, bool success = true)
    {
        public bool Success { get; } = success;
        /// <summary>
        /// Gets the data associated with the current instance.
        /// </summary>
        public T Data { get; } = data;
    }
}