using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegisterLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            var registerLodgingForm = new RegisterLodgingForm();
            registerLodgingForm.Show();
        }

        private void GetAllButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
