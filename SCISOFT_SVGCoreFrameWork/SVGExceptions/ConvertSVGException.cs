using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCISOFT_SVGCoreFrameWork.SVGExceptions
{
    public class ConvertSVGException:Exception
    {
        public ConvertSVGException() : base() { }
        public ConvertSVGException(string message) : base(message) { }
        public ConvertSVGException(string message, Exception inner) : base(message, inner) { }

    }
}
