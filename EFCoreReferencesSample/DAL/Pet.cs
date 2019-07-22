namespace EFCoreReferencesSample.DAL
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
        public int? OwnerId { get; set; }
    }
}
