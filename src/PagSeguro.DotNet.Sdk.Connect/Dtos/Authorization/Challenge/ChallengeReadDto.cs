namespace PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge
{
    public class ChallengeReadDto : AuthorizationReadDto
    {
        public string? Challenge { get; set; }
        public string? DecryptedChallenge { get; set; }
    }
}
