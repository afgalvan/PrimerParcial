using System.Windows;

namespace Presentation
{
    public partial class RegisterLodgingForm : Window
    {
        private readonly string[] _validGuestTypes = { "Particular", "Miembro", "Premium" };

        public RegisterLodgingForm()
        {
            InitializeComponent();
            PopulateFormOptions();
        }

        private void PopulateFormOptions()
        {
            GuestTypeComboBox.ItemsSource = _validGuestTypes;
            GuestTypeComboBox.IsEditable  = false;
        }
    }
}
