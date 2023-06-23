using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class Paging
    {
        private int _skip;
        private int _take;

        public Paging(int pageSize,int pageNum) { 
        _skip = pageSize*(pageNum-1);
            _take = pageSize;
        }
        public int Skip => _skip;

        public int Take => _take;

    }
}
