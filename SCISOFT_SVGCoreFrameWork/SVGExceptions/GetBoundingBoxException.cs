using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCISOFT_SVGCoreFrameWork.SVGExceptions
{
    public class GetBoundingBoxException:Exception
    {
        public GetBoundingBoxException() : base() { }
        public GetBoundingBoxException(string message) : base(message) { }
        public GetBoundingBoxException(string message, Exception inner) : base(message, inner) { }
    }
}
