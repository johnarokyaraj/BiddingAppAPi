using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.Enums
{
    public enum ProductCategoryEnum
    {
        [Description("Painting")]
        Painting=1,
        [Description("Sculptor")]
        Sculptor = 2,
        [Description("Ornament")]
        Ornament = 3
    }
}
