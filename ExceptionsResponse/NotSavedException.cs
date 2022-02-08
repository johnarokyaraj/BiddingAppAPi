using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.ExceptionsResponse
{
    public class NotSavedException :ApplicationException
    {
        public NotSavedException() { }
    public NotSavedException(string message) : base(message) { }
    }
}
