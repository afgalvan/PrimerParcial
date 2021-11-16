using System.Windows;

namespace Presentation
{
    public static class MaterialDialog
    {
        public static void ShowSucceed(string title, string message)
        {
            var messageBox =
                new MessageBoxCustom(title, message, MessageType.Success, MessageButtons.Ok);
            messageBox.ShowDialog();
        }

        public static void ShowError(string title, string message)
        {
            var messageBox =
                new MessageBoxCustom(title, message, MessageType.Error, MessageButtons.Ok);
            // messageBox.ShowDialog();
            MessageBox.Show(message);
        }

        public static void ShowWarning(string title, string message)
        {
            var messageBox =
                new MessageBoxCustom(title, message, MessageType.Warning, MessageButtons.Ok);
            messageBox.ShowDialog();
        }

        public static void ShowInfo(string title, string message)
        {
            var messageBox =
                new MessageBoxCustom(title, message, MessageType.Information, MessageButtons.Ok);
            messageBox.ShowDialog();
        }
    }
}
