using System.Windows;
using System.Windows.Media;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MessageBoxCustom.xaml
    /// </summary>
    public partial class MessageBoxCustom : Window
    {
        private SolidColorBrush Color { get; set; }

        public MessageBoxCustom(string title, string message, MessageType messageType, MessageButtons buttons)
        {
            InitializeComponent();
            Color = SetupColors(messageType);
            cardHeader.Background = Color;
            btnCancel.Background = Color;
            btnOk.Background = Color;
            txtMessage.Text = message;
            txtTitle.Text = title;
            if (buttons == MessageButtons.Ok)
            {
                btnCancel.Visibility = Visibility.Collapsed;
            }
        }

        public SolidColorBrush SetupColors(MessageType messageType) => messageType switch
        {
            MessageType.Success => Brushes.Green,
            MessageType.Info => Brushes.AliceBlue,
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
        Info,
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
