using SCISOFT_DTO;
using Aspose.Svg.Rendering.Image;
using Aspose.Svg;
using SCISOFT_SVGCoreFrameWork.SVGExceptions;

namespace SCISOFT_SVGCoreFrameWork
{
    public class JPEGConvertStrategy : ConvertStrategy
    {
        public override string ConvertSVG(SVG_DTO svgFile)
        {
            try
            {
                string dataDir = svgFile.SVGPath;
                var options = new ImageRenderingOptions(ImageFormat.Jpeg);
                options.BackgroundColor = System.Drawing.ColorTranslator.FromHtml(svgFile.BackgroundcolorSelection);
                using (var document = new SVGDocument(dataDir))
                {
                    using (var device = new ImageDevice(options, dataDir + "copyBmp.Jpeg"))
                    {
                        document.RenderTo(device);
                    }
                }
                return dataDir + "copyBmp_1.Jpeg";
            }
            catch (System.Exception ex)
            {
                throw new ConvertSVGException("Error al convertir svt a JPEG", ex);
            }

        }
    }
}
