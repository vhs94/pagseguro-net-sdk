namespace PagSeguro.DotNet.Sdk.Connect.Models.Responses
{
    public class ChallengeResponse : AuthorizationResponse
    {
        public string? Challenge { get; set; }
        public string? DecryptedChallenge { get; set; }
    }
}
