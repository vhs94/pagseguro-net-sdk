using System.ComponentModel;

namespace PagSeguro.DotNet.Sdk.Common.Helpers
{
    [Flags]
    public enum ApiScopes
    {
        [Description("None")]
        None = 0,
        [Description("certificate.create")]
        CreateCertificate = 1,
        [Description("payments.create")]
        CreatePayments = 2,
        [Description("payments.read")]
        ReadPayments = 3,
        [Description("payments.refund")]
        RefundPayments = 4,
        [Description("accounts.read")]
        ReadAccounts = 5
    }
}
