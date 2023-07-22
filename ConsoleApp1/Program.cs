using PagSeguro.DotNet.Sdk;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.Challenge;
using PagSeguro.DotNet.Sdk.Settings;
using System.Security.Cryptography;

var rsa = RSA.Create(2048);
//var pubKey = rsa.ExportSubjectPublicKeyInfo();
//var privKey = rsa.ImportRSAPublicKey();

//Console.WriteLine(Convert.ToBase64String(pubKey));
//Console.WriteLine("-------------");
//Console.WriteLine(Convert.ToBase64String(privKey));




var pagSeguro = new PagSeguroClient(new ClientSettings
{
    Environment = PagSeguroEnvironment.Sandbox,
    Token = "BCABE5E7AA9D43BCBBB76E3C45C1567A",
    //PrivateKey = File.ReadAllText("Assets/private-key.txt")
});

await pagSeguro.ConnectChallengeAsync(new ChallengeWriteDto
{
    ClientId = "8815a5e2-616b-4754-a6d1-40d92b71674c",
    ClientSecret = "45e5a4de-b8eb-4b78-9ce6-fad416b1953c"
});

var digitalCertificate = await pagSeguro.DigitalCertificate.CreateCertificateAsync();
var teste = digitalCertificate.Key;



var app = await pagSeguro.Application.GetApplicationAsync("8815a5e2-616b-4754-a6d1-40d92b71674c");
//var challenge = await pagSeguro.Connect.Authorization.CreateAccessTokenByChallengeAsync(new ChallengeWriteDto
//{
//    ClientId = "8815a5e2-616b-4754-a6d1-40d92b71674c",
//    ClientSecret = "45e5a4de-b8eb-4b78-9ce6-fad416b1953c"
//});



//Console.WriteLine(JsonConvert.SerializeObject(challenge));
Console.ReadKey();