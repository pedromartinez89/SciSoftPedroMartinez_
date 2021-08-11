using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Svg;
using Aspose.Svg.Rendering.Image;
using System.Drawing;
using SCISOFT_DTO;
using Aspose.Imaging;
using Aspose.Imaging.ImageOptions;
using System.IO;
using System.Xml.Linq;
using SCISOFT_SVGCoreFrameWork.SVGExceptions;


namespace SCISOFT_SVGCoreFrameWork
{
    public class SVG
    {

        
        /// <summary>
        /// recibe un SVG y devuelve un string con el codigo.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string SvgToStr(SVG_DTO svg)
        {
            try
            {                
                XDocument doc = XDocument.Load(svg.SVGPath);
                XElement rootElements = doc.Root;
                //guardo el xml del svg para procesarlo en el cliente.
                return doc.Root.ToString();
            }
            catch (Exception ex)
            {
                throw new SVGtoStringException("Error al convertir un SVG a string",ex);
            }

        }


        /// <summary>
        /// version serverside de getBoundingBox
        /// </summary>
        /// <param name="svgFile"></param>
        /// <returns></returns>
        public SVG_DTO GetBoundingBox(SVG_DTO svgFile)
        {
            try
            {
                var x = new SVGDocument(svgFile.SVGPath);

                var f = x.DocumentElement as SVGGraphicsElement;
                var root = x.RootElement as SVGGraphicsElement;
                var n = root.GetBBox();

                svgFile.BboxHeight = n.Height.ToString();
                svgFile.BboxWidth = n.Width.ToString();
                svgFile.BboxX = n.X.ToString();
                svgFile.BboxY = n.Y.ToString();

                return svgFile;
            }
            catch (Exception ex)
            {
                throw new GetBoundingBoxException("Error al intentar buscar el bounding box",ex);
            }
            
        }        

        /// <summary>
        /// Recibe un string que contiene el codigo de un svg y crea un archivo svg.
        /// por ultimo devuelve el path del archivo svg creado.
        /// </summary>
        /// <param name="SVGfile"></param>
        /// <returns></returns>
        public string CreateSVGFileFromString(SVG_DTO SVGfile)
        {
            try
            {
                var document = new SVGDocument(SVGfile.SVGStringCode, ".");              
                document.Save(SVGfile.SVGPath);
                return SVGfile.SVGPath;
            }
            catch (Exception ex)
            {
                throw new CreateSVGFileFromStringException("Error al crear el SVG desde un string",ex);                
            }
           
        }
        
        /// <summary>
        /// Cambia el tamaño de un SVG al indicado.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public string ResizeSvg(SVG_DTO svg)
        {
            try
            {
                string inputFileName = svg.SVGPath;
                using (Aspose.Imaging.Image image = Aspose.Imaging.Image.Load(inputFileName))
                {
                    image.Resize(svg.Width, svg.Height);
                    image.Save(svg.SVGPath + "_resized.svg", new SvgOptions()
                    {
                        VectorRasterizationOptions = new SvgRasterizationOptions()
                    });
                }
                return svg.SVGPath + "_resized.svg";
            }
            catch (Exception ex)
            {
                throw new ResizeSVGException("Error al intentar cambiar el tamaño del SVG",ex);
            }
            
        }
        
        /// <summary>
        /// Devuelve todos los path de un svg
        /// </summary>
        /// <param name="svg"></param>
        /// <returns></returns>
        public List<string> GetAllPaths(SVG_DTO svg)
        {
            try
            {
                XDocument doc = XDocument.Load(svg.SVGPath);
                XElement rootElements = doc.Root;

                List<string> paths = new List<string>();
                foreach (var node in doc.Root.DescendantNodes().OfType<XElement>()
                              .Select(x => x)
                              .Where(n => n.Name.LocalName == "path"))
                {
                    paths.Add(node.ToString().Remove(node.ToString().IndexOf("xmlns=")) + "/>");
                }

                return paths;
            }
            catch (Exception ex)
            {
                throw new GetAllPathsException("Error al obtener los paths del svg" ,ex);
            }
            
             
        }

               
        /// <summary>
        /// Procesa el svg del lado del server recorriendo cada nodo
        /// en este ejemplo agrega un "fill-opacity" pero podria hacer cualquier procesamiento
        /// dado que trabaja directamente sobre los nodos del xml del svg.
        /// </summary>
        /// <returns></returns>
        public string ServerSideChangeAlpha(SVG_DTO svg)
        {
            try
            {
                XDocument doc = XDocument.Load(svg.SVGPath);
                XElement rootElements = doc.Root;

                //recorro cada nodo, en este caso cambio opacity pero podria hacer cualquier cosa.
                foreach (var node in doc.Root.DescendantNodes().OfType<XElement>()
                               .Select(x => x)
                               .Where(n => n.Name.LocalName == "path"))
                {
                    XAttribute fillOpacity = new XAttribute("fill-opacity", svg.AlphaChannelValue);
                    node.Add(fillOpacity);
                }

                //almaceno el archivo procesado.
                string newFilePath = svg.SVGPath + "_newAlpha.svg";
                doc.Save(newFilePath);
                //podria devolver un objeto file.
                return newFilePath;
            }
            catch (Exception ex)
            {

                throw new ChangeAlphaException("Error al intentar cambiar el palpha del svg",ex);
            }
           
        }

    }
}
