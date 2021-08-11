using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCISOFT_SVGCoreFrameWork.SVGExceptions
{
    class ChangeAlphaException:Exception
    {
        public ChangeAlphaException() : base() { }
        public ChangeAlphaException(string message) : base(message) { }
        public ChangeAlphaException(string message, Exception inner) : base(message, inner) { }
    }
}
