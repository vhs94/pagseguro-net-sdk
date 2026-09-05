using FluentAssertions;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Subscriptions
{
    /// <summary>
    /// Cupons, faturas, pagamentos, estornos e preferências das cobranças recorrentes.
    /// </summary>
    public class CouponAndBillingIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_CouponIsValid_CouponIsCreated()
        {
            CouponRequest couponRequest = CreateCouponRequest();

            CouponResponse result = await Client.ForCoupon().CreateAsync(couponRequest);

            result.Should().NotBeNull();
            result.Id.Should().StartWith("COUP_");
            result.Status.Should().Be("ACTIVE");
            result.Name.Should().Be(couponRequest.Name);
            // A API aceita apenas PERCENT e AMOUNT como tipo de desconto.
            result.Discount!.Type.Should().Be("PERCENT");
            result.Discount.Value.Should().Be(10);
            result.Duration!.Type.Should().Be("REPEATING");
            result.Duration.Occurrences.Should().Be(3);
            result.InUse.Should().BeFalse();
        }

        [Fact]
        public async Task GetByIdAsync_CouponExists_CouponIsReturned()
        {
            CouponResponse created = await Client.ForCoupon().CreateAsync(CreateCouponRequest());

            CouponResponse result = await Client.ForCoupon().GetByIdAsync(created.Id!);

            result.Id.Should().Be(created.Id);
            result.Name.Should().Be(created.Name);
        }

        [Fact]
        public async Task ListAsync_CouponsExist_PageIsReturned()
        {
            await Client.ForCoupon().CreateAsync(CreateCouponRequest());

            CouponListResponse result = await Client.ForCoupon().ListAsync();

            result.Coupons.Should().NotBeNullOrEmpty();
            result.Coupons.Should().OnlyContain(c => c.Id!.StartsWith("COUP_"));
        }

        [Fact]
        public async Task InactivateAndActivateAsync_CouponExists_StatusIsToggled()
        {
            CouponResponse created = await Client.ForCoupon().CreateAsync(CreateCouponRequest());

            await Client.ForCoupon().InactivateAsync(created.Id!);
            CouponResponse inactivated = await Client.ForCoupon().GetByIdAsync(created.Id!);
            inactivated.Status.Should().Be("INACTIVE");

            await Client.ForCoupon().ActivateAsync(created.Id!);
            CouponResponse activated = await Client.ForCoupon().GetByIdAsync(created.Id!);
            activated.Status.Should().Be("ACTIVE");
        }

        [Fact]
        public async Task GetByIdAsync_InvoiceExists_InvoiceIsReturned()
        {
            InvoiceResponse invoice = await CreateSubscriptionInvoiceAsync();

            InvoiceResponse result = await Client.ForInvoice().GetByIdAsync(invoice.Id!);

            result.Id.Should().Be(invoice.Id);
            result.Amount!.Value.Should().Be(1990);
            result.Items.Should().NotBeNullOrEmpty();
            result.Customer!.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ListPaymentsAsync_InvoiceExists_PaymentsAreReturned()
        {
            InvoiceResponse invoice = await CreateSubscriptionInvoiceAsync();

            SubscriptionPaymentListResponse result = await Client
                .ForInvoice()
                .ListPaymentsAsync(invoice.Id!);

            result.Payments.Should().NotBeNullOrEmpty();
            result.Payments.First().Id.Should().StartWith("PAYM_");
        }

        [Fact]
        public async Task ListAsync_PaymentsExist_PageIsReturned()
        {
            await CreateSubscriptionInvoiceAsync();

            SubscriptionPaymentListResponse result = await Client
                .ForSubscriptionPayment()
                .ListAsync(limit: 5);

            result.Payments.Should().NotBeNullOrEmpty();
            result.ResultSet!.Limit.Should().Be(5);
        }

        [Fact]
        public async Task GetByIdAsync_PaymentExists_PaymentIsReturned()
        {
            InvoiceResponse invoice = await CreateSubscriptionInvoiceAsync();
            SubscriptionPaymentListResponse payments = await Client
                .ForInvoice()
                .ListPaymentsAsync(invoice.Id!);
            string paymentId = payments.Payments.First().Id!;

            SubscriptionPaymentResponse result = await Client
                .ForSubscriptionPayment()
                .GetByIdAsync(paymentId);

            result.Id.Should().Be(paymentId);
            result.Invoice!.Id.Should().Be(invoice.Id);
            result.PaymentMethod!.Type.Should().Be("CREDIT_CARD");
            result.Provider.Should().NotBeNull();
        }

        [Fact]
        public async Task ListRefundsAsync_PaymentHasNoRefunds_ListIsEmpty()
        {
            InvoiceResponse invoice = await CreateSubscriptionInvoiceAsync();
            SubscriptionPaymentListResponse payments = await Client
                .ForInvoice()
                .ListPaymentsAsync(invoice.Id!);

            RefundListResponse result = await Client
                .ForSubscriptionPayment()
                .ListRefundsAsync(payments.Payments.First().Id!);

            result.Refunds.Should().BeEmpty();
        }

        [Fact]
        public async Task ListAllRefundsAsync_Always_PageIsReturned()
        {
            RefundListResponse result = await Client
                .ForSubscriptionPayment()
                .ListAllRefundsAsync();

            result.Should().NotBeNull();
            result.Refunds.Should().NotBeNull();
        }

        [Fact]
        public async Task GetNotificationPreferencesAsync_Always_PreferencesAreReturned()
        {
            NotificationPreferenceResponse result = await Client
                .ForSubscriptionPreference()
                .GetNotificationPreferencesAsync();

            result.Should().NotBeNull();
            result.Email.Should().NotBeNull();
            result.Email!.Merchant.Should().NotBeNull();
            result.Email.Customer.Should().NotBeNull();
        }

        [Fact]
        public async Task GetPublicKeyAsync_Always_PublicKeyIsReturned()
        {
            SubscriptionPublicKeyResponse result = await Client
                .ForSubscriptionPreference()
                .GetPublicKeyAsync();

            result.Should().NotBeNull();
            result.PublicKey.Should().NotBeNullOrEmpty();
            result.PublicKey.Should().StartWith("MII");
        }

        private async Task<InvoiceResponse> CreateSubscriptionInvoiceAsync()
        {
            PlanResponse plan = await Client.ForPlan().CreateAsync(CreatePlanRequest());
            CustomerResponse customer = await Client.ForCustomer().CreateAsync(CreateCustomerRequest());
            SubscriptionResponse subscription = await Client
                .ForSubscription()
                .CreateAsync(new SubscriptionRequest
                {
                    ReferenceId = "sub-billing-test",
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
                });

            InvoiceListResponse invoices = await Client
                .ForSubscription()
                .ListInvoicesAsync(subscription.Id!);
            return invoices.Invoices.First();
        }

        private static CouponRequest CreateCouponRequest()
        {
            return new CouponRequest
            {
                ReferenceId = "coup-sdk-test",
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
                ReferenceId = "plan-billing-test",
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
                ReferenceId = "cust-billing-test",
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
