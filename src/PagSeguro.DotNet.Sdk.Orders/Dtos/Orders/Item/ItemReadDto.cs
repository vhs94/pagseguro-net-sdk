namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item
{
    public class ItemReadDto : ItemDto
    {
        public int Weight { get; set; }
        public DimensionDto Dimensions { get; set; }
    }
}
