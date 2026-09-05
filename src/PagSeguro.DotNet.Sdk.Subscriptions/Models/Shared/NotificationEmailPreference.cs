namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Preferências de e-mail das notificações de assinatura.</summary>
    public class NotificationEmailPreference
    {
        /// <summary>Envio de e-mail para o vendedor.</summary>
        public NotificationEmailTarget? Merchant { get; set; }

        /// <summary>Envio de e-mail para o assinante.</summary>
        public NotificationEmailTarget? Customer { get; set; }
    }
}
