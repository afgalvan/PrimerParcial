namespace Entities
{
    public class PremiumLodging : Lodging
    {
        public override string GetGuestType() => "Premium";

        public override double ComputePriceToPay()
        {
            return StayDays * (GetRoomPrice() * 0.01);
        }
    }
}
