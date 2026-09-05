using FluentAssertions;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Subscriptions
{
    /// <summary>
    /// Ciclo completo de cobrança recorrente: plano -> assinante -> assinatura.
    /// </summary>
    public class SubscriptionIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_CustomerIsValid_CustomerIsCreatedWithTokenizedCard()
        {
            CustomerRequest customerRequest = CreateCustomerRequest();

            CustomerResponse result = await Client.ForCustomer().CreateAsync(customerRequest);

            result.Should().NotBeNull();
            result.Id.Should().StartWith("CUST_");
            result.Name.Should().Be(customerRequest.Name);
            result.Email.Should().Be(customerRequest.Email);
            result.TaxId.Should().Be(customerRequest.TaxId);
            result.Phones.Should().ContainSingle();
            result.Address!.City.Should().Be("Sao Paulo");

            // O cartão nunca volta em claro: o PagBank devolve um token e os dados mascarados.
            result.BillingInfo.Should().ContainSingle();
            SubscriptionCard card = result.BillingInfo.First().Card!;
            card.Token.Should().NotBeNullOrEmpty();
            card.Number.Should().BeNull();
            card.Brand.Should().Be("visa");
            card.LastDigits.Should().Be("2097");
        }

        [Fact]
        public async Task GetByIdAsync_CustomerExists_CustomerIsReturned()
        {
            CustomerResponse created = await Client.ForCustomer().CreateAsync(CreateCustomerRequest());

            CustomerResponse result = await Client.ForCustomer().GetByIdAsync(created.Id!);

            result.Id.Should().Be(created.Id);
            result.Email.Should().Be(created.Email);
        }

        [Fact]
        public async Task ListAsync_CustomersExist_PageIsReturned()
        {
            await Client.ForCustomer().CreateAsync(CreateCustomerRequest());

            CustomerListResponse result = await Client.ForCustomer().ListAsync(limit: 5);

            result.Customers.Should().NotBeNullOrEmpty();
            result.ResultSet!.Limit.Should().Be(5);
            result.Customers.Should().OnlyContain(c => c.Id!.StartsWith("CUST_"));
        }

        [Fact]
        public async Task UpdateAsync_CustomerExists_CustomerIsUpdated()
        {
            CustomerResponse created = await Client.ForCustomer().CreateAsync(CreateCustomerRequest());
            CustomerUpdateRequest updateRequest = new()
            {
                Name = "Jose da Silva Alterado",
                Phones = [new CustomerPhone { Country = "55", Area = "11", Number = "988888888" }],
                Address = created.Address
            };

            CustomerResponse result = await Client.ForCustomer().UpdateAsync(created.Id!, updateRequest);

            result.Id.Should().Be(created.Id);
            result.Name.Should().Be("Jose da Silva Alterado");
        }

        [Fact]
        public async Task CreateAsync_SubscriptionIsValid_SubscriptionIsCreated()
        {
            PlanResponse plan = await Client.ForPlan().CreateAsync(CreatePlanRequest());
            CustomerResponse customer = await Client.ForCustomer().CreateAsync(CreateCustomerRequest());

            SubscriptionResponse result = await Client
                .ForSubscription()
                .CreateAsync(CreateSubscriptionRequest(plan.Id!, customer.Id!));

            result.Should().NotBeNull();
            result.Id.Should().StartWith("SUBS_");
            result.Plan!.Id.Should().Be(plan.Id);
            result.Customer!.Id.Should().Be(customer.Id);
            result.Amount!.Value.Should().Be(1990);
            result.PaymentMethod.Should().ContainSingle();
            result.PaymentMethod.First().Type.Should().Be("CREDIT_CARD");
            result.Links.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ListAsync_SubscriptionsExist_PageIsReturned()
        {
            await CreateSubscriptionAsync();

            SubscriptionListResponse result = await Client.ForSubscription().ListAsync(limit: 5);

            result.Subscriptions.Should().NotBeNullOrEmpty();
            result.ResultSet!.Limit.Should().Be(5);
            result.Subscriptions.Should().OnlyContain(s => s.Id!.StartsWith("SUBS_"));
        }

        [Fact]
        public async Task ListInvoicesAsync_SubscriptionExists_InvoicesAreReturned()
        {
            SubscriptionResponse subscription = await CreateSubscriptionAsync();

            InvoiceListResponse result = await Client
                .ForSubscription()
                .ListInvoicesAsync(subscription.Id!);

            result.Invoices.Should().NotBeNullOrEmpty();
            InvoiceResponse invoice = result.Invoices.First();
            invoice.Id.Should().StartWith("INVO_");
            invoice.Amount!.Value.Should().Be(1990);
            invoice.Subscription!.Id.Should().Be(subscription.Id);
        }

        [Fact]
        public async Task SuspendAndActivateAsync_SubscriptionExists_StatusIsToggled()
        {
            SubscriptionResponse subscription = await CreateSubscriptionAsync();

            await Client.ForSubscription().SuspendAsync(subscription.Id!);

            SubscriptionResponse suspended = await Client.ForSubscription().GetByIdAsync(subscription.Id!);
            suspended.Status.Should().Be("SUSPENDED");

            await Client.ForSubscription().ActivateAsync(subscription.Id!);

            SubscriptionResponse activated = await Client.ForSubscription().GetByIdAsync(subscription.Id!);
            activated.Status.Should().NotBe("SUSPENDED");
        }

        [Fact]
        public async Task CancelAsync_SubscriptionExists_SubscriptionIsCancelled()
        {
            SubscriptionResponse subscription = await CreateSubscriptionAsync();

            await Client.ForSubscription().CancelAsync(subscription.Id!);

            SubscriptionResponse cancelled = await Client.ForSubscription().GetByIdAsync(subscription.Id!);
            cancelled.Status.Should().Be("CANCELED");
        }

        private async Task<SubscriptionResponse> CreateSubscriptionAsync()
        {
            PlanResponse plan = await Client.ForPlan().CreateAsync(CreatePlanRequest());
            CustomerResponse customer = await Client.ForCustomer().CreateAsync(CreateCustomerRequest());
            return await Client
                .ForSubscription()
                .CreateAsync(CreateSubscriptionRequest(plan.Id!, customer.Id!));
        }

        private static SubscriptionRequest CreateSubscriptionRequest(string planId, string customerId)
        {
            return new SubscriptionRequest
            {
                ReferenceId = "sub-sdk-test",
                Plan = new PlanReference { Id = planId },
                Customer = new CustomerReference { Id = customerId },
                PaymentMethod =
                [
                    new SubscriptionPaymentMethod
                    {
                        Type = "CREDIT_CARD",
                        Card = new SubscriptionCard { SecurityCode = "123" }
                    }
                ]
            };
        }

        private static PlanRequest CreatePlanRequest()
        {
            return new PlanRequest
            {
                ReferenceId = "plan-sdk-test",
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
                ReferenceId = "cust-sdk-test",
                Name = "Jose da Silva",
                // O e-mail precisa ser diferente do e-mail do vendedor e unico por assinante.
                Email = $"jose.{Guid.NewGuid():N}@teste.com",
                // A API aceita um unico assinante por CPF: repetir o documento devolve 409.
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
        /// Gera um CPF valido e novo a cada execucao. A API de Assinaturas recusa um
        /// segundo assinante com o mesmo tax_id, entao um CPF fixo faria o teste passar
        /// apenas na primeira execucao.
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
