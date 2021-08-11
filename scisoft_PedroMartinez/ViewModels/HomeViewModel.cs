using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace scisoft_PedroMartinez.ViewModels
{
    public class HomeViewModel
    {
        public HttpPostedFileBase SVGFile { set; get; }
        public string SVGFileModified { set; get; }
        public string AlphaChanelValue { set; get; } = "0.5";
        public string BackgroundcolorSelection { get; set; }
    }
}
