using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft;
using scisoft_PedroMartinez.ViewModels;
using SCISOFT_DTO;
using SCISOFT_SVGCoreFrameWork;
using SCISOFT_SVGCoreFrameWork.ConvertStrategys;

namespace scisoft_PedroMartinez.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            if (model.SVGFile != null && model.SVGFile.ContentLength > 0)
                try
                {
                    SVG SVG_Helper = new SVG();
                    SVG_DTO SVGDTO = new SVG_DTO();

                    SVGDTO.SVGPath = Path.Combine(Server.MapPath("~/uploadedFiles"),
                                               Path.GetFileName(model.SVGFile.FileName));                    

                    model.SVGFile.SaveAs(SVGDTO.SVGPath); //opcionalmente guardo el archivo. podria ser interesante tener las imagenes.
                    
                    ViewBag.uploaded = "true";                    
                    ViewBag.svgCode = HttpUtility.JavaScriptStringEncode(SVG_Helper.SvgToStr(SVGDTO));
                    //SVG_Helper.ResizeSvg(SVGDTO);
                    //SVG_Helper.ServerSideChangeAlpha(SVGDTO); //si quisiera hacerlo server side. (guarda en uploadfiles el nuevo svg)                   
                    ViewBag.allPaths = Newtonsoft.Json.JsonConvert.SerializeObject(SVG_Helper.GetAllPaths(SVGDTO));                  
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    ViewBag.uploaded = "false";
                }
            else
            {
                ViewBag.Message = "no selecciono ningun archivo.";
                ViewBag.uploaded = "false";
            }
            return View();
        }      

       
        #region CONVERT_SVG
        [HttpPost]
        public ActionResult ConvertToJpeg(string svgCode, string bkgColor, string alpha)
        {
            try
            {
                string base64Decoded;
                byte[] data = Convert.FromBase64String(svgCode);
                base64Decoded = System.Text.Encoding.ASCII.GetString(data);

                base64Decoded = base64Decoded.Replace("<SVG", "<SVG opacity='" + alpha + "'");
                base64Decoded = base64Decoded.Replace("<svg", "<svg opacity='" + alpha + "'");

                SVG SVG_Helper = new SVG();
                SVG_DTO dtoSVG = new SVG_DTO();
                dtoSVG.SVGStringCode = base64Decoded;
                dtoSVG.SVGPath = Path.Combine(Server.MapPath("~/uploadedFiles/"),
                     "_modified.svg");
                dtoSVG.BackgroundcolorSelection = bkgColor;
                string fileSVGModified = SVG_Helper.CreateSVGFileFromString(dtoSVG);

                var context = new ConvertContext();                
                context.SetExportStrategy(new JPEGConvertStrategy());
                string jpegFile = context.Convert(dtoSVG);

                return Json(jpegFile);
            }
            catch (Exception ex)
            {                
                //log ex.message
                return View("Error");
            }
            
        }

        [HttpPost]
        public ActionResult ConvertToBmp(string svgCode, string bkgColor, string alpha)
        {
            try
            {
                string base64Decoded;
                byte[] data = Convert.FromBase64String(svgCode);
                base64Decoded = System.Text.Encoding.ASCII.GetString(data);

                base64Decoded = base64Decoded.Replace("<SVG", "<SVG opacity='" + alpha + "'");
                base64Decoded = base64Decoded.Replace("<svg", "<svg opacity='" + alpha + "'");

                SVG SVG_Helper = new SVG();
                SVG_DTO dtoSVG = new SVG_DTO();
                dtoSVG.SVGStringCode = base64Decoded;
                dtoSVG.SVGPath = Path.Combine(Server.MapPath("~/uploadedFiles/"),
                     "_modified.svg");
                dtoSVG.BackgroundcolorSelection = bkgColor;
                string fileSVGModified = SVG_Helper.CreateSVGFileFromString(dtoSVG);

                var context = new ConvertContext();
                context.SetExportStrategy(new BMPConvertStrategy());
                string bmpFile = context.Convert(dtoSVG);

                return Json(bmpFile);
            }
            catch (Exception ex)
            {
                //log ex.message
                return View("Error");
            }
            
        }

        public ActionResult ConvertToBmp64(string svgCode, string bkgColor, string alpha)
        {
            try
            {
                string base64Decoded;
                byte[] data = Convert.FromBase64String(svgCode);
                base64Decoded = System.Text.Encoding.ASCII.GetString(data);

                base64Decoded = base64Decoded.Replace("<SVG", "<SVG opacity='" + alpha + "'");
                base64Decoded = base64Decoded.Replace("<svg", "<svg opacity='" + alpha + "'");

                SVG SVG_Helper = new SVG();
                SVG_DTO dtoSVG = new SVG_DTO();
                dtoSVG.SVGStringCode = base64Decoded;
                dtoSVG.SVGPath = Path.Combine(Server.MapPath("~/uploadedFiles/"),
                     "_modified.svg");
                dtoSVG.BackgroundcolorSelection = bkgColor;
                string fileSVGModified = SVG_Helper.CreateSVGFileFromString(dtoSVG);

                var context = new ConvertContext();
                context.SetExportStrategy(new BMP64ConvertStrategy());
                string bmpFile = context.Convert(dtoSVG);

                return Json(bmpFile);
            }
            catch (Exception ex)
            {
                //log ex.message
                return View("Error");
            }
           
        }

        public FileResult DownloadFile(string pathFileToDownload)
        {
            var svg = new SCISOFT_SVGCoreFrameWork.SVG();
            byte[] bytesArchivo = System.IO.File.ReadAllBytes(pathFileToDownload);
            return File(bytesArchivo, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(pathFileToDownload));
        }
        #endregion

        #region OtherFunctios

        [HttpPost]
        public ActionResult Resize(string path, int width,int height)
        {
            try
            {
                SVG_DTO svg_dto = new SVG_DTO();
                SVG SVG_Helper = new SVG();

                svg_dto.SVGPath = Path.Combine(Server.MapPath("~/uploadedFiles"),
                                              Path.GetFileName(path));

                svg_dto.Width = width;
                svg_dto.Height = height;

                string resized = SVG_Helper.ResizeSvg(svg_dto);
                return Json(resized);
            }
            catch (Exception ex)
            {
                //log ex.message
                return View("Error");                
            }
            
        }

        public ActionResult SVG1Download()
        {
            string imgToDownload = Server.MapPath("~/uploadedFiles/lapicero.svg");
           return DownloadFile(imgToDownload);
        }
        #endregion







    }
}