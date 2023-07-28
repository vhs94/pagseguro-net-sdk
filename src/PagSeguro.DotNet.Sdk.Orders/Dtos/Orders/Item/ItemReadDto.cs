namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class ItemReadDto : ItemDto
    {
        public int Weight { get; set; }
        public DimensionDto Dimensions { get; set; }
    }
}
