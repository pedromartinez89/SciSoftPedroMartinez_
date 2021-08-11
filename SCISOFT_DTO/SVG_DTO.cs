using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCISOFT_DTO
{
    /// <summary>
    /// SVG Data Transfer Object.
    /// </summary>
    public class SVG_DTO
    {
        public string BackgroundcolorSelection { set; get; }
        public string SVGPath { get; set; }
        public string SVGStringCode { set; get; }
        public string BboxWidth { set; get; }
        public string BboxHeight { set; get; }
        public string BboxX { set; get; }
        public string BboxY { set; get; }
        public string AlphaChannelValue { get; set; } = "0.5"; //hardcoded, si quisiera implementar serverside, debo enviarlo desde el cliente
        public int Width { get; set; } = 400; //hardcoded si se implementa lo recibiria del modelview
        public int Height { get; set; } = 400; //hardcoded si se implementa lo recibiria del modelview
    }
}
