using System;
using System.Linq;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;
using Logic.Exceptions;

namespace Logic.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiredArgumentsAttribute : Attribute, IMethodAsyncAdvice
    {
        public string ErrorMessage { get; set; } = "Valor nulo";

        public async Task Advise(MethodAsyncAdviceContext context)
        {
            if (context.Arguments.Any(arg => arg == null))
            {
                throw new InvalidArgumentException(ErrorMessage);
            }

            await context.ProceedAsync();
        }
    }
}
