using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Parameters
{
    public class OrderCartParams:PagingParams
    {
        public Guid? CustomerId { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
    }
}
