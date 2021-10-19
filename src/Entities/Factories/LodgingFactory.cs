namespace Entities.Factories
{
    public static class LodgingFactory
    {
        public static Lodging CreateLodging(string guestType) => guestType switch
        {
            "particular" => new GeneralLodging(),
            "premium" => new PremiumLodging(),
            _ => new FellowLodging()
        };
    }
}
