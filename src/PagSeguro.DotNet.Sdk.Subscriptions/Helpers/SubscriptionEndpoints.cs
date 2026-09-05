namespace PagSeguro.DotNet.Sdk.Subscriptions.Helpers
{
    /// <summary>
    /// Endereços da API de Assinaturas, que roda em um host próprio, separado
    /// do restante da API do PagBank.
    /// </summary>
    public static class SubscriptionEndpoints
    {
        /// <summary>URL base do ambiente de testes.</summary>
        public const string SandboxBaseUrl = "https://sandbox.api.assinaturas.pagseguro.com";

        /// <summary>URL base do ambiente de produção.</summary>
        public const string ProductionBaseUrl = "https://api.assinaturas.pagseguro.com";

        public const string Plans = "/plans";
        public const string Customers = "/customers";
        public const string Subscriptions = "/subscriptions";
        public const string Coupons = "/coupons";
        public const string Invoices = "/invoices";
        public const string Payments = "/payments";
        public const string Refunds = "/refunds";
        public const string PublicKeys = "/public-keys";

        public const string Activate = "activate";
        public const string Inactivate = "inactivate";
        public const string Cancel = "cancel";
        public const string Suspend = "suspend";
    }
}
