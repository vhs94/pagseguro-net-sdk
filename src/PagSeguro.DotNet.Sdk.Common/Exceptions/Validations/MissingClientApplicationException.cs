using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions
{
    public class MissingClientApplicationException : Exception
    {
        public MissingClientApplicationException()
            : base(ErrorMessages.MissingClientApplicationExceptionMessage)
        {
        }
    }
}
