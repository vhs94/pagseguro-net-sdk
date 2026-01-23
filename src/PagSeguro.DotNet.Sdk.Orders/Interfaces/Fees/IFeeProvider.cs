using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Interfaces.Fees
{
    public interface IFeeProvider
    {
        /// <summary>
        /// Set the credit card bin. The first 6 digits of the card number
        /// </summary>
        IFeeProvider WithCreditCardBin(int creditCardBin);

        /// <summary>
        /// Set the maximum quantity of allowed installments, regardless of the transfer.
        /// </summary>
        IFeeProvider WithMaxInstallments(int maxInstallments);

        /// <summary>
        /// Set the maximum quantity of allowed installments without interest.
        /// </summary>
        IFeeProvider WithMaxInstallmentsNoInterest(int maxInstallmentsNoInterest);

        /// <summary>
        /// Set the transaction amount value
        /// </summary>
        IFeeProvider WithValue(int amountValue);

        /// <summary>
        /// Allows you to calculate fees for a given transaction
        /// </summary>
        Task<FeeResponse> CalculateAsync();
    }
}
