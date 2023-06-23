

namespace Domain.Entity.Order
{
    public class OrderDetail : BaseEntity
    {


        public Guid ItemId { get; set; }

        public Guid OrderCartId { get; set; }


        public decimal NetTotalPrice { get; set; }


        public int Quantity { get; set; }


        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }


        public virtual Item Item { get; set; }

        public virtual OrderCart OrderCart { get; set; }




    }
}
