using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Shipping
{
    public class ShippingAddressDto : AddressDto
    {
        public string Complement { get; set; }
    }
}
