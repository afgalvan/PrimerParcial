using System.Windows;
using System.Windows.Media;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MessageBoxCustom.xaml
    /// </summary>
    public partial class MessageBoxCustom : Window
    {
        private SolidColorBrush Color { get; }

        public MessageBoxCustom(string title, string message, MessageType messageType,
            MessageButtons buttons)
        {
            InitializeComponent();
            Color                   = SetupColors(messageType);
            DialogTitle.Text              = title;
            TextMessage.Text        = message;
            TextMessage.CaretBrush  = Color;
            CardHeader.Background   = Color;
            CancelButton.Background = Color;
            OkButton.Background     = Color;
            if (buttons == MessageButtons.Ok)
            {
                CancelButton.Visibility = Visibility.Collapsed;
            }
        }

        private static SolidColorBrush SetupColors(MessageType messageType) => messageType switch
        {
            MessageType.Success => Brushes.LimeGreen,
            MessageType.Information => Brushes.RoyalBlue,
            MessageType.Error => Brushes.Red,
            MessageType.Warning => Brushes.Yellow,
            _ => Brushes.White
        };

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

    public enum MessageType
    {
        Information,
        Success,
        Warning,
        Error,
    }

    public enum MessageButtons
    {
        OkCancel,
        Ok,
    }
}
