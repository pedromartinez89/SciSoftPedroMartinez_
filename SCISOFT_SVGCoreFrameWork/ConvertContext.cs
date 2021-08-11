using SCISOFT_DTO;

namespace SCISOFT_SVGCoreFrameWork
{
    public class ConvertContext
    {
        private ConvertStrategy _strategy;

        public void SetExportStrategy(ConvertStrategy convertStrategy)
        {
            _strategy = convertStrategy;
        }

        public string Convert(SVG_DTO file)
        {
            return _strategy.ConvertSVG(file);
        }

    }
}
