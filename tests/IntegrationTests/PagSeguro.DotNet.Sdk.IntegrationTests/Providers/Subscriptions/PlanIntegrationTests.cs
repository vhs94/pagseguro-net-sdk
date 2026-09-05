using FluentAssertions;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.IntegrationTests.Providers.Subscriptions
{
    public class PlanIntegrationTests : BaseIntegrationTests
    {
        [Fact]
        public async Task CreateAsync_RequestIsValid_PlanIsCreated()
        {
            PlanRequest planRequest = CreatePlanRequest();

            PlanResponse result = await Client.ForPlan().CreateAsync(planRequest);

            result.Should().NotBeNull();
            result.Id.Should().StartWith("PLAN_");
            result.Status.Should().Be("ACTIVE");
            result.Name.Should().Be(planRequest.Name);
            result.ReferenceId.Should().Be(planRequest.ReferenceId);
            result.Amount!.Value.Should().Be(1990);
            result.Amount.Currency.Should().Be("BRL");
            result.Interval!.Unit.Should().Be("MONTH");
            result.Interval.Length.Should().Be(1);
            // created_at chega com o offset -03:00 e o System.Text.Json converte
            // para o horario LOCAL da maquina. Comparar com DateTime.UtcNow.Date
            // confrontaria uma data local com uma data UTC, o que falha sempre que
            // os dois lados caem em dias diferentes. A janela abaixo compara o
            // instante, nao o dia, e ainda verifica que o recurso foi criado agora.
            result.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(10));
            result.Links.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_PlanExists_PlanIsReturned()
        {
            PlanResponse created = await Client.ForPlan().CreateAsync(CreatePlanRequest());

            PlanResponse result = await Client.ForPlan().GetByIdAsync(created.Id!);

            result.Id.Should().Be(created.Id);
            result.Name.Should().Be(created.Name);
            result.Amount!.Value.Should().Be(created.Amount!.Value);
        }

        [Fact]
        public async Task ListAsync_PlansExist_PageIsReturned()
        {
            await Client.ForPlan().CreateAsync(CreatePlanRequest());

            PlanListResponse result = await Client.ForPlan().ListAsync(limit: 10);

            result.Plans.Should().NotBeNullOrEmpty();
            result.ResultSet.Should().NotBeNull();
            result.ResultSet!.Total.Should().BeGreaterThan(0);
            result.ResultSet.Limit.Should().Be(10);
            result.Plans.Should().OnlyContain(plan => plan.Id!.StartsWith("PLAN_"));
        }

        [Fact]
        public async Task UpdateAsync_PlanExists_PlanIsUpdated()
        {
            PlanResponse created = await Client.ForPlan().CreateAsync(CreatePlanRequest());
            PlanRequest updateRequest = CreatePlanRequest();
            updateRequest.Name = "Plano SDK alterado";

            PlanResponse result = await Client.ForPlan().UpdateAsync(created.Id!, updateRequest);

            result.Id.Should().Be(created.Id);
            result.Name.Should().Be("Plano SDK alterado");

            PlanResponse reloaded = await Client.ForPlan().GetByIdAsync(created.Id!);
            reloaded.Name.Should().Be("Plano SDK alterado");
        }

        [Fact]
        public async Task InactivateAndActivateAsync_PlanExists_StatusIsToggled()
        {
            PlanResponse created = await Client.ForPlan().CreateAsync(CreatePlanRequest());
            created.Status.Should().Be("ACTIVE");

            await Client.ForPlan().InactivateAsync(created.Id!);

            PlanResponse inactivated = await Client.ForPlan().GetByIdAsync(created.Id!);
            inactivated.Status.Should().Be("INACTIVE");

            await Client.ForPlan().ActivateAsync(created.Id!);

            PlanResponse activated = await Client.ForPlan().GetByIdAsync(created.Id!);
            activated.Status.Should().Be("ACTIVE");
        }

        private static PlanRequest CreatePlanRequest()
        {
            return new PlanRequest
            {
                ReferenceId = "plan-sdk-test",
                Name = "Plano SDK",
                Description = "Plano usado nos testes de integracao do SDK",
                Amount = new Money { Value = 1990, Currency = "BRL" },
                Interval = new PlanInterval { Unit = "MONTH", Length = 1 },
                Trial = new PlanTrial { Enabled = false },
                PaymentMethod = ["CREDIT_CARD"]
            };
        }
    }
}
