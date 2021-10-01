using System;

namespace Entities
{
    public abstract class Lodging
    {
        public int          Id           { get; set; }
        public int          PeopleAmount { get; set; }
        public DateTime     EntryDate    { get; set; }
        public DateTime     ExitDate     { get; set; }
        public RoomCapacity RoomCapacity { get; set; }
        public int          StayDays     => (ExitDate - EntryDate).Days;

        private readonly double[] _prices = { 2000, 4000, 6000, 12_000 };

        public double ComputeRoomPrice()
        {
            int currentRoomIndex = GetRoomIndex();
            return _prices[currentRoomIndex];
        }

        private int GetRoomIndex()
        {
            return (int)RoomCapacity;
        }

        public abstract string GuestType();
        public abstract double ComputePriceToPay();

        public override string ToString()
        {
            return
                $"Id: {Id}\nTipo de huesped: {GuestType()}\nTipo de habitación: {RoomCapacity.GetString()}\nFecha de ingreso: {EntryDate}\nFecha de salida: {ExitDate}\nCupos: {PeopleAmount}\nDías de hospedaje: {StayDays}\nLiquidación: ${ComputePriceToPay()}\n{new string('-', 30)}\n";
        }
    }
}
