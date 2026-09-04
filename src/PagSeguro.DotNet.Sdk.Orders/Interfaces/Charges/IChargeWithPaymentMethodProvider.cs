using PagSeguro.DotNet.Sdk.Common.Interfaces;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges.PaymentMethods;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Charges
{
    /// <summary>
    /// Ponto de entrada para criar uma cobrança avulsa, escolhendo o meio de pagamento.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public interface IChargeWithPaymentMethodProvider : IProvider
    {
        /// <summary>
        /// Seleciona o fluxo de cobrança paga com boleto.
        /// </summary>
        IBankSlipChargeProvider WithBankSlip();
        /// <summary>
        /// Seleciona o fluxo de cobrança paga com cartão de crédito.
        /// </summary>
        ICreditCardChargeProvider WithCreditCard();
        /// <summary>
        /// Seleciona o fluxo de cobrança paga com cartão
        /// de crédito e autenticação 3DS.
        /// </summary>
        ICreditCardWith3DsAuthChargeProvider WithCreditCardAnd3DsAuthentication();
        /// <summary>
        /// Seleciona o fluxo de cobrança paga com cartão
        /// de débito e autenticação 3DS.
        /// A autenticação 3DS é obrigatória para cartão de débito.
        /// </summary>
        IDebitCardWith3DsAuthChargeProvider WithDebitCardAnd3DsAuthentication();
    }
}
