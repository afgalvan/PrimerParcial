using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public abstract class Lodging
    {
        [Key]
        public int Id { get; set; }

        public int          PeopleAmount { get; set; }
        public DateTime     EntryDate    { get; set; }
        public DateTime     ExitDate     { get; set; }
        public RoomCapacity RoomCapacity { get; set; }
        public int          StayDays     => (ExitDate - EntryDate).Days;

        public double RoomPrice  => GetRoomPrice();
        [MaxLength(25)]
        public string GuestType  => GetGuestType();
        public double PriceToPay => ComputePriceToPay();

        private static readonly double[] Prices = { 2000, 4000, 6000, 12_000 };

        public double GetRoomPrice()
        {
            int currentRoomIndex = GetRoomIndex();
            return Prices[currentRoomIndex];
        }

        private int GetRoomIndex()
        {
            return (int)RoomCapacity;
        }

        protected abstract string GetGuestType();
        public abstract double ComputePriceToPay();

        public override string ToString()
        {
            return
                $"Id: {Id}\nTipo de huesped: {GetGuestType()}\nTipo de habitación: {RoomCapacity.AsString()}\nFecha de ingreso: {EntryDate}\nFecha de salida: {ExitDate}\nCupos: {PeopleAmount}\nDías de hospedaje: {StayDays}\nLiquidación: ${ComputePriceToPay()}\n{new string('-', 30)}\n";
        }
    }
}
