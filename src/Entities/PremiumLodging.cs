namespace Entities
{
    public class PremiumLodging : Lodging
    {
        public override string GuestType() => "Premium";

        public override double ComputePriceToPay()
        {
            return StayDays * (ComputeRoomPrice() * 0.1);
        }
    }
}
