namespace Entities
{
    public class GeneralLodging : Lodging
    {
        public override string GuestType() => "Particular";

        public override double ComputePriceToPay()
        {
            return StayDays * (ComputeRoomPrice() * GetAdditionalPercentage());
        }

        private double GetAdditionalPercentage() => StayDays switch
        {
            >= 2 and <= 3 => 1.1,
            >= 4 => 1.2,
            _ => 1
        };
    }
}
