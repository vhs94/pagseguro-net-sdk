using System.ComponentModel;
using System.Reflection;

namespace PagSeguro.DotNet.Sdk.Common.Helpers
{
    public static class EnumExtensions
    {
        public static string ToStringApiScopes(this ApiScopes? apiScopes)
        {
            IEnumerable<Enum> scopes = Enum.GetValues(typeof(ApiScopes))
                .Cast<Enum>()
                .Where(e => (ApiScopes)e != ApiScopes.None);
            if (apiScopes.HasValue)
            {
                scopes = scopes.Where(apiScopes.Value.HasFlag);
            }
            IEnumerable<string> scopesDescription = scopes
                .Select(apiScope => apiScope.ToDescription());
            return string.Join("+", scopesDescription);
        }

        public static string ToDescription(this Enum enumValue)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(enumValue));
        }

        public static IList<string> GetValues<TEnum>()
            where TEnum : Enum
        {
            return ((TEnum[])Enum.GetValues(typeof(TEnum)))
                .Select(en => en.ToString().ToLower())
                .ToList();
        }
    }
}
