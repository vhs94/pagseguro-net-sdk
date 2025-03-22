using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Fees;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Converters
{
    public class CreditCardBrandDtoConverter : JsonConverter<CreditCardDto>
    {
        public override CreditCardDto Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var rootElement = jsonDocument.RootElement;

            string brandName = GetBrandName(rootElement);
            var brandElement = rootElement.GetProperty(brandName);

            var target = new CreditCardDto
            {
                Brand = JsonSerializer.Deserialize<CreditCardBrandDto>(
                    brandElement.GetRawText(),
                    options: JsonOptions.Default)
            };

            // Populate additional properties from the original JSON
            foreach (var property in rootElement.EnumerateObject())
            {
                if (property.Name != brandName)
                {
                    // Populate non-brand properties dynamically as needed
                    JsonSerializer.Deserialize(
                        property.Value.GetRawText(),
                        target.GetType(),
                        options);
                }
            }

            return target;
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditCardDto value,
            JsonSerializerOptions options)
        {
            // Implement serialization logic if required
            //throw new NotImplementedException();
        }

        private static string GetBrandName(JsonElement rootElement)
        {
            var availableBrands = EnumExtensions.GetValues<BrandNameDto>();
            return availableBrands.First(brand => HasField(brand, rootElement));
        }

        private static bool HasField(string fieldName, JsonElement rootElement)
        {
            return rootElement.TryGetProperty(fieldName, out _);
        }
    }


    //public class CreditCardBrandDtoConverter : JsonConverter<CreditCardDto>
    //{
    //    public override CreditCardDto ReadJson(
    //        JsonReader reader,
    //        Type objectType,
    //        CreditCardDto? existingValue,
    //        bool hasExistingValue,
    //        JsonSerializer serializer)
    //    {
    //        var jObject = JObject.Load(reader);
    //        string brandName = GetBrandName(jObject);
    //        var target = new CreditCardDto
    //        {
    //            Brand = JsonConvert.DeserializeObject<CreditCardBrandDto>(jObject[brandName]!.ToString())!
    //        };
    //        serializer.Populate(jObject.CreateReader(), target);
    //        return target;
    //    }

    //    private static string GetBrandName(JObject jObject)
    //    {
    //        var availableBrands = EnumExtensions.GetValues<BrandNameDto>();
    //        return availableBrands.First(brand => HasField(brand, jObject));
    //    }

    //    private static bool HasField(string fieldName, JObject jObject) => jObject[fieldName] != null;

    //    public override void WriteJson(
    //        JsonWriter writer,
    //        CreditCardDto? value,
    //        JsonSerializer serializer)
    //    {
    //    }
    //}
}
