namespace EFCoreReferencesSample.DAL
{
    public class CarEntity
    {
        protected CarEntity()
        {
        }

        public int Id { get; private set; }

        public string LicensePlate { get; protected set; }

        public int CarAccidents { get; protected set; }
    }
}
