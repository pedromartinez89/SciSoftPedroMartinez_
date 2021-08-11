using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCISOFT_DTO;
using SCISOFT_SVGCoreFrameWork.SVGExceptions;
using System.Drawing;
using System.IO;

namespace SCISOFT_SVGCoreFrameWork.ConvertStrategys
{
    public class BMP64ConvertStrategy : ConvertStrategy
    {        
        public override string ConvertSVG(SVG_DTO svgFile)
        {
            try
            {
                using (var memoryStream = new MemoryStream(File.ReadAllBytes(svgFile.SVGPath)))
                {
                    byte[] imageBytes = memoryStream.ToArray();
                    string base64 = Convert.ToBase64String(imageBytes);
                    return base64;
                }
            }
            catch (Exception ex)
            {
                throw new ConvertSVGException("Error al convertir svt a BMP64", ex);
            }
                     
        }
    }
}
