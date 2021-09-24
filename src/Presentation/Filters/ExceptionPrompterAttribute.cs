using System;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;

namespace Presentation.Filters
{
    public class ExceptionPrompterAttribute : ErrorHandlingAttribute, IMethodAsyncAdvice
    {
        public async Task Advise(MethodAsyncAdviceContext context)
        {
            try
            {
                await context.ProceedAsync();
            }
            catch (Exception e)
            {
                WriteError(e.Message);
            }
        }
    }
}
