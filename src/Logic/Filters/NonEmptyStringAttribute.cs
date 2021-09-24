using System;
using ArxOne.MrAdvice.Advice;
using Logic.Exceptions;

namespace Logic.Filters
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class NonEmptyStringAttribute : Attribute, IParameterAdvice
    {
        public string ErrorMessage { get; }

        public NonEmptyStringAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public void Advise(ParameterAdviceContext context)
        {
            var value = (string)context.Value;
            if (string.IsNullOrEmpty(value)) throw new InvalidArgumentException(ErrorMessage);
            context.Proceed();
        }
    }
}
