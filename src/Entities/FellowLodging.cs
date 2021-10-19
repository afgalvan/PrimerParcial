namespace Entities
{
    public class FellowLodging : Lodging
    {
        private const int FreeDays   = 3;
        private const int FreeGuests = 2;

        private int OutOfLimitGuests => PeopleAmount - FreeGuests;
        private int OutOfLimitDays   => StayDays - FreeDays;

        public override string GetGuestType() => "Socio";

        public override double ComputePriceToPay()
        {
            double payByTime = HasPassedIsFreeDays() ? ComputeRegularPay() : 0;
            return payByTime + GetAdditionalPeopleFee() * StayDays;
        }

        private double ComputeRegularPay()
        {
            return RoomCapacity == RoomCapacity.Suite
                ? ComputeRegularPayForSuite()
                : ComputeRegularPayForAnyRoom();
        }

        private double ComputeRegularPayForSuite()
        {
            return OutOfLimitDays * GetRoomPrice() * 1.05;
        }

        private double ComputeRegularPayForAnyRoom()
        {
            return StayDays * GetRoomPrice() * 1.02;
        }

        private bool HasPassedIsFreeDays() => StayDays > FreeDays;

        private double GetAdditionalPeopleFee() => OutOfLimitGuests switch
        {
            1 => 100,
            >= 2 and <= 3 => 200,
            >= 4 and <= 6 => 300,
            _ => 0
        };
    }
}
