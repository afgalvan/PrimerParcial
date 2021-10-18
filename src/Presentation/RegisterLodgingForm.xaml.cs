using BespokeFusion;
using Entities;
using Entities.Factories;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Presentation
{
    public partial class RegisterLodgingForm : Window
    {
        private readonly LodgingService _lodgingService;
        private RoomCapacity RoomCapacity { get; set; }

        public RegisterLodgingForm(LodgingService lodgingService)
        {
            _lodgingService = lodgingService;
            InitializeComponent();
            PopulateFormOptions();
        }

        private void PopulateFormOptions()
        {
            GuestTypeComboBox.ItemsSource = _lodgingService.GetAvailableGuestTypes();
            GuestTypeComboBox.IsEditable  = false;
            RoomTypeComboBox.ItemsSource  = _lodgingService.GetAvailableRooms();
            RoomTypeComboBox.IsEditable   = false;
            FillRoomsCapacities();
            GuestsAmountComboBox.IsEditable = false;
        }

        private void FillRoomsCapacities()
        {
            GuestsAmountComboBox.ItemsSource = GenerateRoomsCapacities();
        }

        private void UpdateRoomsCapacities(object sender, SelectionChangedEventArgs e)
        {
            FillRoomsCapacities();
        }

        private IEnumerable<int> GenerateRoomsCapacities()
        {
            RoomCapacity = RoomCapacityFactory.CreateRoomOfIndex(RoomTypeComboBox.SelectedIndex);
            return Enumerable.Range(1, RoomCapacity.MaxCapacity());
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            await _lodgingService.AddLodging(MapFormToLodging(), App.CancellationToken);
            // MaterialDialog.ShowOk("Registrar liquidación", "Liquidación registrada con éxito.", "Aceptar");
        }

        private Lodging MapFormToLodging()
        {
            Lodging lodging = LodgingFactory.CreateLodging(GuestTypeComboBox.Text.ToLower());
            lodging.RoomCapacity = RoomCapacity;
            lodging.PeopleAmount = Convert.ToInt32(GuestsAmountComboBox.Text);
            lodging.EntryDate = Convert.ToDateTime(EntryDate.Text);
            lodging.ExitDate = Convert.ToDateTime(ExitDate.Text);

            return lodging;
        }
    }
}
