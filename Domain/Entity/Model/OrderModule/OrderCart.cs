
using Domain.Enum.Order;

namespace Domain.Entity.Model.Order
{
    public class OrderCart : BaseEntity
    {

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }



        public OrderStatus OrderStatus { get; set; }

        public Guid TaxId { get; set; }

        public virtual Tax Tax { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }



    }
}
