using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Fees;

namespace PagSeguro.DotNet.Sdk.Orders.Converters
{
    public class CreditCardBrandDtoConverter : JsonConverter<CreditCardDto>
    {
        public override CreditCardDto ReadJson(
            JsonReader reader,
            Type objectType,
            CreditCardDto? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            string brandName = GetBrandName(jObject);
            var target = new CreditCardDto
            {
                Brand = JsonConvert.DeserializeObject<CreditCardBrandDto>(jObject[brandName]!.ToString())!
            };
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        private static string GetBrandName(JObject jObject)
        {
            var availableBrands = EnumExtensions.GetValues<BrandNameDto>();
            return availableBrands.First(brand => HasField(brand, jObject));
        }

        private static bool HasField(string fieldName, JObject jObject) => jObject[fieldName] != null;

        public override void WriteJson(
            JsonWriter writer,
            CreditCardDto? value,
            JsonSerializer serializer)
        {
        }
    }
}
