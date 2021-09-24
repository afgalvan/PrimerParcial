using System;

namespace Entities
{
    public enum RoomCapacity : byte
    {
        Familiar = 0,
        Simple,
        Doubly,
        Suite
    }

    public static class RoomCapacityExtensions
    {
        public static int MaxCapacity(this RoomCapacity roomCapacity) => roomCapacity switch
        {
            RoomCapacity.Familiar => 4,
            RoomCapacity.Simple => 1,
            RoomCapacity.Doubly => 2,
            RoomCapacity.Suite => 6,
            _ => 0
        };
    }
}
