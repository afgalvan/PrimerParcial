using System;

namespace Presentation.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ErrorHandlingAttribute : Attribute
    {
        protected void DisplayError(string title, string message)
        {
            MaterialDialog.ShowError(title, message);
        }
    }
}
