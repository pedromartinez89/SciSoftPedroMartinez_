using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCISOFT_SVGCoreFrameWork.SVGExceptions
{
    public class CreateSVGFileFromStringException : Exception
    {
        public CreateSVGFileFromStringException() : base() { }
        public CreateSVGFileFromStringException(string message) : base(message) { }
        public CreateSVGFileFromStringException(string message, Exception inner) : base(message, inner) { }

    }
}
