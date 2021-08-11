using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCISOFT_SVGCoreFrameWork.SVGExceptions
{
    public class ResizeSVGException:Exception
    {
        public ResizeSVGException() : base() { }
        public ResizeSVGException(string message) : base(message) { }
        public ResizeSVGException(string message, Exception inner) : base(message, inner) { }
    }
}
