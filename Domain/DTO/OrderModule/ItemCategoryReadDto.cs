

namespace Domain.Entity.Order
{
    public class ItemCategoryReadDto:BaseEntity
    {
        ItemCategoryReadDto() { 
        Items=new HashSet<Item>();  
        }

      public required string Name { get; set; }
       public required string Description { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
