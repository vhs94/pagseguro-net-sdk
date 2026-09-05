using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Converters
{
    public class CreditCardBrandConverter : JsonConverter<CreditCardInfo>
    {
        public override CreditCardInfo Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var rootElement = jsonDocument.RootElement;

            string brandName = GetBrandName(rootElement);
            var brandElement = rootElement.GetProperty(brandName);

            var target = new CreditCardInfo
            {
                Brand = JsonSerializer.Deserialize<CreditCardBrand>(
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
            CreditCardInfo value,
            JsonSerializerOptions options)
        {
            // Implement serialization logic if required
            //throw new NotImplementedException();
        }

        private static string GetBrandName(JsonElement rootElement)
        {
            var availableBrands = EnumExtensions.GetValues<BrandName>();
            return availableBrands.First(brand => HasField(brand, rootElement));
        }

        private static bool HasField(string fieldName, JsonElement rootElement)
        {
            return rootElement.TryGetProperty(fieldName, out _);
        }
    }
}
