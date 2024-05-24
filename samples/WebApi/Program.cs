using Microsoft.AspNetCore.Mvc;
using PagSeguro.DotNet.Sdk;
using PagSeguro.DotNet.Sdk.Common.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuracao credenciais pagseguro
builder.Services.AddPagSeguro(options =>
{
    options.ClientId = "8815a5e2-616b-4754-a6d1-40d92b71674c";
    options.ClientSecret = "45e5a4de-b8eb-4b78-9ce6-fad416b1953c";
    options.Token = "BCABE5E7AA9D43BCBBB76E3C45C1567A";
    options.Environment = PagSeguroEnvironment.Sandbox;
});


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

//consome api do pagseguro utilizando o client injetado
app.MapGet("/public-key", async ([FromServices] IPagSeguroClient pagSeguroClient) =>
{
    var publicKey = await pagSeguroClient
        .ForPublicKey()
        .GetAsync();
    return publicKey;
})
.WithOpenApi();

app.Run();
