using Logic;
using System;
using System.Threading.Tasks;
using System.Windows;
using Entities;


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

        private async void OnLoaded(object sender, EventArgs e)
        {
            await LoadTableInformation();
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            await DeleteSelectedLodging();
            await LoadTableInformation();
        }

        private async void AddButton_Click(object sender, EventArgs e)
        {
            var registerLodgingForm = new RegisterLodgingForm(_lodgingService);
            registerLodgingForm.ShowDialog();
            await LoadTableInformation();
        }

        private async Task LoadTableInformation()
        {
            Table.ItemsSource = await _lodgingService.GetAllLodging(App.CancellationToken);
        }

        private async Task DeleteSelectedLodging()
        {
            int? selectedId = GetSelectedLodgingId();
            if (selectedId.HasValue)
            {
                await _lodgingService.DeleteById(selectedId.Value, App.CancellationToken);
                return;
            }

            MaterialDialog.ShowError("Borrar liquidación", "Ninguna liquidación seleccionada");
        }

        private int? GetSelectedLodgingId()
        {
            return ((Lodging)Table.SelectedItem)?.Id;
        }
    }
}
