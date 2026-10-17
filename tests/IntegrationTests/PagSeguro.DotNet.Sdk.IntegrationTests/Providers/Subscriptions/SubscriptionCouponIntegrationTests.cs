using FluentAssertions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Subscriptions
{
    /// <summary>
    /// Cobertura viva de DELETE /subscriptions/{id}/coupons e de GET /refunds/{id}.
    /// </summary>
    public class SubscriptionCouponIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_CouponIsInformed_SubscriptionCarriesTheCoupon()
        {
            CouponResponse coupon = await Client.ForCoupon().CreateAsync(CreateCouponRequest());

            SubscriptionResponse result = await CreateSubscriptionWithCouponAsync(coupon.Id!);

            result.Coupon.Should().NotBeNull();
            result.Coupon!.Id.Should().Be(coupon.Id);
            result.Coupon.Discount!.Value.Should().Be(10);
        }

        [Fact]
        public async Task RemoveCouponAsync_SubscriptionHasCoupon_SubscriptionIsReturned()
        {
            CouponResponse coupon = await Client.ForCoupon().CreateAsync(CreateCouponRequest());
            SubscriptionResponse subscription = await CreateSubscriptionWithCouponAsync(coupon.Id!);

            SubscriptionResponse result = await Client
                .ForSubscription()
                .RemoveCouponAsync(subscription.Id!);

            // A documentacao diz que a resposta e um objeto vazio, mas a API
            // devolve a assinatura inteira.
            result.Should().NotBeNull();
            result.Id.Should().Be(subscription.Id);

            // A remocao so vale a partir da proxima recorrencia, entao o cupom
            // continua visivel no ciclo corrente.
            result.Coupon.Should().NotBeNull();
        }

        [Fact]
        public async Task RemoveCouponAsync_SubscriptionHasNoCoupon_ApiRejectsTheRemoval()
        {
            SubscriptionResponse subscription = await CreateSubscriptionWithCouponAsync(couponId: null);

            Func<Task> task = async () => await Client
                .ForSubscription()
                .RemoveCouponAsync(subscription.Id!);

            // 422: a rota existe e o identificador e valido; o que a API recusa e
            // remover um cupom que nao esta ativo na assinatura.
            var assertion = await task.Should().ThrowAsync<PagSeguroHttpException>();
            assertion.Which.Response.Should().Contain("coupon");
        }

        [Fact]
        public async Task GetRefundByIdAsync_RefundDoesNotExist_ApiRejectsOnlyTheId()
        {
            // O caminho feliz nao e automatizavel: o sandbox de Assinaturas nunca
            // aprova a cobranca do cartao (todo pagamento fica DENIED ou UNPAID),
            // entao nao ha como criar um estorno de verdade. O teste valida o
            // encanamento: rota, bearer token e desserializacao do erro.
            Func<Task> task = async () => await Client
                .ForSubscriptionPayment()
                .GetRefundByIdAsync("REFU_00000000-0000-0000-0000-000000000000");

            var assertion = await task.Should().ThrowAsync<NotFoundException>();
            assertion.Which.Response.Should().Contain("refund_not_found");
            assertion.Which.Response.Should().Contain("refund_id");
        }

        private async Task<SubscriptionResponse> CreateSubscriptionWithCouponAsync(string? couponId)
        {
            PlanResponse plan = await Client.ForPlan().CreateAsync(CreatePlanRequest());
            CustomerResponse customer = await Client.ForCustomer().CreateAsync(CreateCustomerRequest());

            SubscriptionRequest subscriptionRequest = new()
            {
                ReferenceId = "sub-coupon-test",
                Plan = new PlanReference { Id = plan.Id },
                Customer = new CustomerReference { Id = customer.Id },
                PaymentMethod =
                [
                    new SubscriptionPaymentMethod
                    {
                        Type = "CREDIT_CARD",
                        Card = new SubscriptionCard { SecurityCode = "123" }
                    }
                ]
            };

            if (couponId is not null)
            {
                subscriptionRequest.Coupon = new CouponReference { Id = couponId };
            }

            return await Client.ForSubscription().CreateAsync(subscriptionRequest);
        }

        private static CouponRequest CreateCouponRequest()
        {
            return new CouponRequest
            {
                ReferenceId = "coup-remove-test",
                Name = "Cupom SDK",
                Description = "Cupom usado nos testes de integracao",
                Discount = new CouponDiscount { Type = "PERCENT", Value = 10 },
                Duration = new CouponDuration { Type = "REPEATING", Occurrences = 3 },
                RedemptionLimit = 100,
                ExpiresAt = "2027-01-01"
            };
        }

        private static PlanRequest CreatePlanRequest()
        {
            return new PlanRequest
            {
                ReferenceId = "plan-coupon-test",
                Name = "Plano SDK",
                Amount = new Money { Value = 1990, Currency = "BRL" },
                Interval = new PlanInterval { Unit = "MONTH", Length = 1 },
                Trial = new PlanTrial { Enabled = false },
                PaymentMethod = ["CREDIT_CARD"]
            };
        }

        private static CustomerRequest CreateCustomerRequest()
        {
            return new CustomerRequest
            {
                ReferenceId = "cust-coupon-test",
                Name = "Jose da Silva",
                Email = $"jose.{Guid.NewGuid():N}@teste.com",
                TaxId = GenerateCpf(),
                BirthDate = "1990-01-01",
                Phones = [new CustomerPhone { Country = "55", Area = "11", Number = "999999999" }],
                Address = new CustomerAddress
                {
                    Street = "Av Brigadeiro Faria Lima",
                    Number = "1384",
                    Locality = "Pinheiros",
                    City = "Sao Paulo",
                    RegionCode = "SP",
                    Country = "BRA",
                    PostalCode = "01452002"
                },
                BillingInfo =
                [
                    new BillingInfo
                    {
                        Type = "CREDIT_CARD",
                        Card = new SubscriptionCard
                        {
                            Number = "4539620659922097",
                            ExpMonth = "12",
                            ExpYear = "2026",
                            SecurityCode = "123",
                            Holder = new CardHolder { Name = "Jose da Silva" }
                        }
                    }
                ]
            };
        }

        /// <summary>
        /// Gera um CPF valido e novo a cada execucao: a API recusa um segundo
        /// assinante com o mesmo tax_id.
        /// </summary>
        private static string GenerateCpf()
        {
            int[] digits = new int[11];
            for (int i = 0; i < 9; i++)
            {
                digits[i] = Random.Shared.Next(0, 10);
            }

            for (int position = 9; position < 11; position++)
            {
                int sum = 0;
                for (int i = 0; i < position; i++)
                {
                    sum += digits[i] * (position + 1 - i);
                }

                int remainder = sum % 11;
                digits[position] = remainder < 2 ? 0 : 11 - remainder;
            }

            return string.Concat(digits);
        }
    }
}
