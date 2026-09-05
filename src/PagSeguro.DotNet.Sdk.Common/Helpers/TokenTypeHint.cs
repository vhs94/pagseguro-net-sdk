using System.ComponentModel;

namespace PagSeguro.DotNet.Sdk.Common.Helpers
{
    /// <summary>
    /// Indica qual tipo de token está sendo revogado.
    /// </summary>
    public enum TokenTypeHint
    {
        /// <summary>
        /// Revoga o access_token, mantendo o refresh_token válido.
        /// </summary>
        [Description("access_token")]
        AccessToken,

        /// <summary>
        /// Revoga o refresh_token e, com ele, o access_token associado.
        /// </summary>
        [Description("refresh_token")]
        RefreshToken
    }
}
