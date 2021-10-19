namespace Entities
{
    public class PremiumLodging : Lodging
    {
        protected override string GetGuestType() => "Premium";

        public override double ComputePriceToPay()
        {
            return StayDays * (GetRoomPrice() * 0.01);
        }
    }
}
