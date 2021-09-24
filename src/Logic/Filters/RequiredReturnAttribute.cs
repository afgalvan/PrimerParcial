using System;
using System.Globalization;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;
using Logic.Exceptions;

namespace Logic.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequiredReturnAttribute : Attribute, IMethodAsyncAdvice
    {
        public string ErrorMessage { get; set; }
        public Type   Type         { get; }

        public RequiredReturnAttribute(Type type)
        {
            Type = type;
        }

        public async Task Advise(MethodAsyncAdviceContext context)
        {
            await context.ProceedAsync();
            object returnValue = GetReturnValue(context.ReturnValue);
            if (returnValue == null) throw new NotFoundException(ErrorMessage);
        }

        private object GetReturnValue(object asyncReturn)
        {
            return asyncReturn is Task
                ? ((dynamic)asyncReturn).Result
                : Convert.ChangeType(asyncReturn, Type, CultureInfo.InvariantCulture);
        }
    }
}
