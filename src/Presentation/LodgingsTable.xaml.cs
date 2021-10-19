using Logic;
using System;
using System.Windows;


namespace Presentation
{
    /// <summary>
    /// Interaction logic for LodgingsTable.xaml
    /// </summary>
    public partial class LodgingsTable : Window
    {
        private readonly LodgingService _lodgingService;

        public LodgingsTable(LodgingService lodgingService)
        {
            InitializeComponent();
            _lodgingService = lodgingService;
        }

        private async void LoadTableInformation(object sender, EventArgs e)
        {
            Table.ItemsSource = await _lodgingService.GetAllLodging(App.CancellationToken);
        }
    }
}
