using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCISOFT_SVGCoreFrameWork.SVGExceptions
{
    public class GetAllPathsException:Exception
    {
        public GetAllPathsException() : base() { }
        public GetAllPathsException(string message) : base(message) { }
        public GetAllPathsException(string message, Exception inner) : base(message, inner) { }

    }
}
