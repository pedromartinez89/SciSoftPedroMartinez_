using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCISOFT_SVGCoreFrameWork.SVGExceptions
{
    [Serializable]
    public class SVGtoStringException:Exception
    {
        public SVGtoStringException() : base(){ }
        public SVGtoStringException(string message) : base(message) { }
        public SVGtoStringException(string message, Exception inner) : base(message, inner) { }
    }
}
