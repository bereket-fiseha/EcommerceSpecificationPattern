using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Parameters
{
    public class OrderDetailParams:PagingParams
    {

        public Guid OrderCartId { get; set; }

    }
}
