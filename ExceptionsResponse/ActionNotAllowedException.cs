using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidingAPPAPI.ExceptionsResponse
{
    public class ActionNotAllowedException : ApplicationException
    {
        public ActionNotAllowedException() { }
        public ActionNotAllowedException(string message) : base(message) { }
    }
}
