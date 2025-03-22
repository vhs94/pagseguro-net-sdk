using MethodDecorator.Fody.Interfaces;
using PagSeguro.DotNet.Sdk.Common.Attributes;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using System.Reflection;

[module: RequiredValidation]
namespace PagSeguro.DotNet.Sdk.Common.Attributes
{
    [AttributeUsage(
        AttributeTargets.Method
        | AttributeTargets.Constructor
        | AttributeTargets.Assembly
        | AttributeTargets.Module)]
    public class RequiredValidationAttribute : Attribute, IMethodDecorator
    {
        public IProvider Provider { get; private set; } = null!;

        public void Init(object instance, MethodBase method, object[] args)
        {
            Provider = (IProvider)instance;
            if (!IsValid())
            {
                throw CreateCustomException();
            }
        }

        protected virtual bool IsValid() => throw new NotImplementedException();

        protected virtual Exception CreateCustomException() => throw new NotImplementedException();

        public void OnEntry() { }

        public void OnException(Exception exception) { }

        public void OnExit() { }
    }
}
