namespace Domain.Entity.Model.Order
{
    public class ItemCategory : BaseEntity
    {

        public ItemCategory()
        {
            Items = new HashSet<Item>();
        }

        public required string Name { get; set; }
        public required string Description { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
