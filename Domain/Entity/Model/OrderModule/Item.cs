namespace Domain.Entity.Model.Order
{
    public class Item : BaseEntity
    {
        public decimal Price { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }

        public Guid ItemCategoryId { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
    }
}
