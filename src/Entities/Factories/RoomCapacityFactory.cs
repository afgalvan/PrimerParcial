using System;
using System.Linq;

namespace Entities.Factories
{
    public static class RoomCapacityFactory
    {
        public static RoomCapacity CreateRoomOfIndex(int position)
        {
            RoomCapacity[] capacities = Enum.GetValues(typeof(RoomCapacity))
                .Cast<RoomCapacity>()
                .ToArray();

            return capacities[position];
        }
    }
}
