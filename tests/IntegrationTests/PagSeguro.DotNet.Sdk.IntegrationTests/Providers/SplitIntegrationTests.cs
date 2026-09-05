using AutoFixture;
using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using PagSeguro.DotNet.Sdk.Orders.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers
{
    /// <summary>
    /// Cobertura viva de GET /splits/{id}, de POST /splits/{id}/custody/release e
    /// do objeto <c>splits</c> enviado na cobranca.
    /// </summary>
    /// <remarks>
    /// O caminho feliz nao e automatizavel: dividir um pagamento exige que a conta
    /// principal esteja habilitada como marketplace e que os recebedores sejam
    /// contas PagBank aprovadas, o que nao existe nas credenciais compartilhadas de
    /// sandbox. Os testes entao provam que a serializacao chega ao validador de
    /// split da API: o erro devolvido cita justamente o campo do recebedor, o que
    /// so acontece se o objeto foi enviado com o nome e o aninhamento corretos.
    /// </remarks>
    public class SplitIntegrationTests : BaseIntegrationTests
    {
        private const string UnknownSplitId = "SPLI_00000000-0000-0000-0000-000000000000";
        private const string UnknownAccountId = "ACCO_99999999-9999-9999-9999-999999999999";

        [Fact]
        public async Task GetByIdAsync_SplitDoesNotExist_ApiRejectsOnlyTheId()
        {
            Func<Task> task = async () => await Client.ForSplit().GetByIdAsync(UnknownSplitId);

            var assertion = await task.Should().ThrowAsync<NotFoundException>();
            assertion.Which.Response.Should().Contain("invalid_id");
            assertion.Which.Response.Should().Contain("split");
        }

        [Fact]
        public async Task ReleaseCustodyAsync_ThereIsNoCustody_ApiAcceptsThePayloadAndRejectsTheState()
        {
            SplitCustodyReleaseRequest releaseRequest = new()
            {
                Receivers =
                [
                    new SplitCustodyReceiverRequest
                    {
                        Account = new SplitAccount { Id = UnknownAccountId }
                    }
                ]
            };

            Func<Task> task = async () => await Client
                .ForSplit()
                .ReleaseCustodyAsync(UnknownSplitId, releaseRequest);

            // no_custody_to_release significa que o corpo passou por toda a
            // validacao de formato e chegou na regra de negocio. Um corpo mal
            // serializado pararia antes, em "receivers[0].account.id".
            var assertion = await task.Should().ThrowAsync<BadRequestException>();
            assertion.Which.Response.Should().Contain("no_custody_to_release");
        }

        [Fact]
        public async Task ChargeAsync_SplitsAreInformed_PayloadReachesTheSplitValidator()
        {
            ChargeByCreditCardRequest chargeRequest = CreateChargeRequest();
            chargeRequest.Splits = new SplitRequest
            {
                Method = SplitMethod.Fixed,
                Receivers =
                [
                    new SplitReceiverRequest
                    {
                        Account = new SplitAccount { Id = UnknownAccountId },
                        Amount = new SplitAmount { Value = 1000 },
                        Reason = "Parceiro"
                    }
                ]
            };

            Func<Task> task = async () => await Client
                .ForCharge()
                .WithCreditCard()
                .Load(chargeRequest)
                .ChargeAsync();

            // A mesma cobranca sem splits e aprovada (ver ChargeIntegrationTests).
            // O erro citar receivers.account.id prova que o objeto splits foi
            // serializado com o nome certo e chegou ao validador.
            var assertion = await task.Should().ThrowAsync<BadRequestException>();
            assertion.Which.Response.Should().Contain("receivers.account.id");
            assertion.Which.Response.Should().Contain(UnknownAccountId);
        }

        [Fact]
        public async Task ChargeAsync_SplitsAreNotInformed_SplitsIsOmittedFromThePayload()
        {
            // Controle do teste acima: sem splits a mesma cobranca e aprovada, o
            // que garante que o 400 anterior vem do split e nao do resto do corpo.
            ChargeByCreditCardResponse result = await Client
                .ForCharge()
                .WithCreditCard()
                .Load(CreateChargeRequest())
                .ChargeAsync();

            result.Id.Should().StartWith("CHAR_");
            result.Status.Should().Be("PAID");
        }

        private ChargeByCreditCardRequest CreateChargeRequest()
        {
            return Client
                .ForCharge()
                .WithCreditCard()
                .AddPaymentMethod(new CreditCardPaymentMethodRequest
                {
                    Installments = 1,
                    Capture = true,
                    SoftDescriptor = "MyStore",
                    Card = new CardRequest
                    {
                        Number = "4111111111111111",
                        ExpMonth = 12,
                        ExpYear = 2030,
                        SecurityCode = 123,
                        Holder = new Holder { Name = "Jose da Silva" }
                    }
                })
                .WithAmount(new ChargeAmountRequest { Value = 5000, Currency = "BRL" })
                .WithReferenceId("split-int-test")
                .WithDescription("Cobranca com divisao de pagamento")
                .Build();
        }
    }
}
