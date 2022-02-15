using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Models
{
    public class PagingFilter
    {
        public string productId { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalRows { get; set; }

    }
}
