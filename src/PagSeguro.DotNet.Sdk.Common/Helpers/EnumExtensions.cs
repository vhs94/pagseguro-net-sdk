using System.ComponentModel;

namespace PagSeguro.DotNet.Sdk.Common.Helpers
{
    public static class EnumExtensions
    {
        public static string ToStringApiScopes(this ApiScopes? apiScopes)
        {
            var scopes = Enum.GetValues<ApiScopes>()
                .Cast<Enum>()
                .Where(e => (ApiScopes)e != ApiScopes.None);
            if (apiScopes.HasValue)
            {
                scopes = scopes.Where(apiScopes.Value.HasFlag);
            }
            var scopesDescription = scopes
                .Select(apiScope => apiScope.ToDescription());
            return string.Join("+", scopesDescription);
        }

        public static string ToDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }

        public static IList<string> GetValues<TEnum>()
            where TEnum : Enum
        {
            return [.. ((TEnum[])Enum.GetValues(typeof(TEnum))).Select(en => en.ToString().ToLower())];
        }
    }
}
