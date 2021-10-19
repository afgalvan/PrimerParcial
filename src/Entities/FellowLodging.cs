using System;

namespace Entities
{
    public class FellowLodging : Lodging
    {
        private const int FreeDays   = 3;
        private const int FreeGuests = 2;

        private int OutOfLimitGuests => PeopleAmount - FreeGuests;
        private int OutOfLimitDays   => Math.Max(StayDays - FreeDays, 0);

        protected override string GetGuestType() => "Socio";

        public override double ComputePriceToPay()
        {
            double payByTime = RoomCapacity == RoomCapacity.Suite
                ? ComputeRegularPayForSuite()
                : ComputeRegularPayForAnyRoom();
            return payByTime + GetAdditionalPeopleFee() * StayDays;
        }

        private double ComputeRegularPayForSuite()
        {
            return OutOfLimitDays * GetRoomPrice() * 0.05;
        }

        private double ComputeRegularPayForAnyRoom()
        {
            return StayDays * GetRoomPrice() * 0.02;
        }

        private double GetAdditionalPeopleFee() => OutOfLimitGuests switch
        {
            1 => 100,
            >= 2 and <= 3 => 200,
            >= 4 and <= 6 => 300,
            _ => 0
        };
    }
}
