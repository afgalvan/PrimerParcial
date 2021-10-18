using BespokeFusion;
using System.Windows;
using System.Windows.Media;

namespace Presentation
{
    class MaterialDialog
    {
        public static void ShowOk(string title, string message, string okContent)
        {
            CustomMaterialMessageBox messageBox = new CustomMaterialMessageBox
            {
                TxtMessage = { Text = message, Foreground = Brushes.Black },
                TxtTitle = { Text = title, Foreground = Brushes.Black },
                MainContentControl = { Background = Brushes.White },
                TitleBackgroundPanel = { Background = Brushes.LightGreen },
                BorderBrush = Brushes.LightGreen
            };
            messageBox.Show();
        }
    }
}
