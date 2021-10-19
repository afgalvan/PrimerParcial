using Entities;
using Entities.Factories;
using Logic;
using Presentation.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public partial class RegisterLodgingForm : Window
    {
        private readonly LodgingService _lodgingService;
        private          RoomCapacity   RoomCapacity { get; set; }

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

        private void UpdateRoomInformation(object sender, SelectionChangedEventArgs e)
        {
            FillRoomsCapacities();
            UpdateRoomPrice();
            UpdatePriceToPay(sender, e);
        }

        private void FillRoomsCapacities()
        {
            GuestsAmountComboBox.ItemsSource = GenerateRoomsCapacities();
        }

        private void UpdateRoomPrice()
        {
            Lodging lodging = GetCurrentLodging();
            lodging.RoomCapacity = RoomCapacity;
            RoomPrice.Text       = $"${lodging.GetRoomPrice()}";
        }

        private void UpdateDatesInformation(object sender, EventArgs e)
        {
            UpdatePriceToPay(sender, e);
            ExitDate.DisplayDateStart = GetExitDate();
            StayDays.Text = $"{GetStayDays()}";
        }

        private void UpdatePriceToPay(object sender, EventArgs e)
        {
            PriceToPay.Text = $"${GetPriceToPay()}";
        }

        private IEnumerable<int> GenerateRoomsCapacities()
        {
            RoomCapacity = RoomCapacityFactory.CreateRoomOfIndex(RoomTypeComboBox.SelectedIndex);
            return Enumerable.Range(1, RoomCapacity.MaxCapacity());
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            await RegisterLodgingFromForm();
        }

        [DisplayOnException("Registrar liquidación")]
        private async Task RegisterLodgingFromForm()
        {
            Lodging lodging = MapFormToLodging();
            await RegisterLodging(lodging);
            MaterialDialog.ShowSucceed("Registrar liquidación",
                "Liquidación registrada con éxito.");
            ClearFields();
        }

        private Lodging GetCurrentLodging()
        {
            return LodgingFactory.CreateLodging(
                GuestTypeComboBox.Text.ToLower(CultureInfo.CurrentCulture));
        }

        private Lodging MapFormToLodging()
        {
            Lodging lodging = GetCurrentLodging();
            lodging.RoomCapacity = RoomCapacity;
            lodging.PeopleAmount =
                Convert.ToInt32(GuestsAmountComboBox.Text, CultureInfo.CurrentCulture);
            lodging.EntryDate = Convert.ToDateTime(EntryDate.Text, CultureInfo.CurrentCulture);
            lodging.ExitDate  = Convert.ToDateTime(ExitDate.Text, CultureInfo.CurrentCulture);

            return lodging;
        }

        private async Task RegisterLodging(Lodging lodging)
        {
            await _lodgingService.AddLodging(lodging, App.CancellationToken);
        }

        private void ClearFields()
        {
            GuestTypeComboBox.SelectedIndex = 0;
            RoomTypeComboBox.SelectedIndex  = 0;
            EntryDate.Text                  = string.Empty;
            ExitDate.Text                   = string.Empty;
            PriceToPay.Text                 = string.Empty;
            RoomPrice.Text                  = string.Empty;
            StayDays.Text                   = string.Empty;
            FillRoomsCapacities();
        }

        private double GetPriceToPay()
        {
            try
            {
                Lodging lodging = MapFormToLodging();
                return lodging.ComputePriceToPay();
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        private int GetStayDays()
        {
            try
            {
                Lodging lodging = MapFormToLodging();
                return lodging.StayDays;
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        private DateTime? GetExitDate()
        {
            try
            {
                return Convert.ToDateTime(EntryDate.Text, CultureInfo.CurrentCulture).AddDays(1);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
