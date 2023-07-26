namespace PagSeguro.DotNet.Sdk.Common.Helpers
{
    public static class ErrorMessages
    {
        public const string MissingClientApplicationExceptionMessage = "Client Id and Secret is required";
        public const string ClientNotConnectedExceptionMessage =
            "Client not connected. Try calling ConnectAsync first.";
        public const string ClientNotConnectedWithChallengeExceptionMessage =
            "Client not connected with challenge. Try calling ConnectChallengeAsync first.";
    }
}
