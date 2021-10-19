using Logic;
using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LodgingService _lodgingService;

        public MainWindow(LodgingService lodgingService)
        {
            _lodgingService = lodgingService;
            InitializeComponent();
        }

        private void RegisterLodgingButton_Click(object sender, RoutedEventArgs e)
        {
            var registerLodgingForm = new RegisterLodgingForm(_lodgingService);
            registerLodgingForm.ShowDialog();
        }

        private void GetAllButton_Click(object sender, RoutedEventArgs e)
        {
            var lodgingTable = new LodgingsTable(_lodgingService);
            lodgingTable.ShowDialog();
        }
    }
}
