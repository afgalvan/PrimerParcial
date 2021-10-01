using System;
using ArxOne.MrAdvice.Advice;

namespace Presentation.Filters
{
    public class RepeatOnExceptionAttribute : ErrorHandlingAttribute, IMethodAdvice
    {
        public void Advise(MethodAdviceContext context)
        {
            while (true)
            {
                try
                {
                    context.Proceed();
                    break;
                }
                catch (Exception e)
                {
                    WriteError(e.Message);
                }
            }
        }
    }
}
