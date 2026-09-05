namespace PagSeguro.DotNet.Sdk.Orders.Helpers
{
    public static class OrderEndpoint
    {
        public const string Orders = "/orders";
        public const string Charges = "charges";
        public const string Pay = "pay";
        public const string Capture = "capture";
        public const string Cancel = "cancel";
        public const string CalculateFee = "fees/calculate";
        public const string ChargeIdQueryParam = "charge_id";
        public const string Splits = "/splits";
        public const string CustodyRelease = "custody/release";
        public const string CardTokens = "/tokens/cards";

        /// <summary>Rota da sessão 3DS, que fica em um host próprio.</summary>
        public const string AuthenticationSessions = "/checkout-sdk/sessions";

        /// <summary>Host do SDK de front-end no ambiente de testes.</summary>
        public const string SandboxSdkBaseUrl = "https://sandbox.sdk.pagseguro.com";

        /// <summary>Host do SDK de front-end no ambiente de produção.</summary>
        public const string ProductionSdkBaseUrl = "https://sdk.pagseguro.com";
    }
}
