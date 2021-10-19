namespace Entities
{
    public class GeneralLodging : Lodging
    {
        protected override string GetGuestType() => "Particular";

        public override double ComputePriceToPay()
        {
            return StayDays * (GetRoomPrice() * GetAdditionalPercentage());
        }

        private double GetAdditionalPercentage() => StayDays switch
        {
            >= 2 and <= 3 => 1.1,
            >= 4 => 1.2,
            _ => 1
        };
    }
}
