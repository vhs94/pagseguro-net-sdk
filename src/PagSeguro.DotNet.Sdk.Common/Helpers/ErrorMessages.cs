namespace PagSeguro.DotNet.Sdk.Common.Helpers
{
    public static class ErrorMessages
    {
        public const string MissingClientApplicationExceptionMessage = "Client Id and Secret is required";
        public const string ClientNotConnectedExceptionMessage =
            "Client not connected. Try calling ConnectAsync first.";
        public const string PrivateKeyNotFoundMessage =
            "Private Key not Found. Please load it on the configuration.";
        public const string ClientNotConnectedWithChallengeExceptionMessage =
            "Client not connected with challenge. Try calling ConnectChallengeAsync first.";
        public const string BadRequestExceptionMessage = "HTTP 400 Bad Request. Check the response for details.";
        public const string ConflicExceptionMessage = "HTTP 409 Conflict. Idempotence key already in use for this resource.";
        public const string ForbiddenExceptionMessage = "HTTP 403 Forbidden. Check your access for this resource.";
        public const string InternalServerErrorExceptionMessage = "HTTP 500 Internal Server Error.";
        public const string NotAcceptableExceptionMessage = "HTTP 406 Not Acceptable. Check the used HTTP verb used.";
        public const string NotFoundExceptionMessage = "HTTP 404 Not Found.";
        public const string UnauthorizedExceptionMessage = "HTTP 401 Unauthorized.";
        public const string UnkownHttpExceptionMessage = "HTTP Exception. Check the response for details.";
    }
}
