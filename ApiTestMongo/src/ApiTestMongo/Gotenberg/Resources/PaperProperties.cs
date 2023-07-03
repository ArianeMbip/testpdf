//namespace DossierPortailAPI.Services.Gotenberg.Ressources
namespace ApiTestMongo.Gotenberg.Resources
{
    public class PaperProperties
    {
        /// <summary>
        /// Paper width, in inches (default 8.5)
        /// </summary>
        public double PaperWidth { get; set; }

        /// <summary>
        /// Paper height, in inches (default 11)
        /// </summary>
        public double PaperHeight { get; set; }

        /// <summary>
        /// Top margin, in inches (default 0.39)
        /// </summary>
        public double MarginTop { get; set; }

        /// <summary>
        /// Bottom margin, in inches (default 0.39)
        /// </summary>
        public double MarginBottom { get; set; }

        /// <summary>
        /// Left margin, in inches (default 0.39)
        /// </summary>
        public double MarginLeft { get; set; }

        /// <summary>
        /// Right margin, in inches (default 0.39)
        /// </summary>
        public double MarginRight { get; set; }

        /// <summary>
        /// Define whether to prefer page size as defined by CSS (default false)
        /// </summary>
        public bool PreferCssPageSize { get; set; }

        /// <summary>
        /// Print the background graphics (default false)
        /// </summary>
        public bool PrintBackground { get; set; }

        /// <summary>
        /// Set the paper orientation to landscape (default false)
        /// </summary>
        public bool Landscape { get; set; }

        /// <summary>
        /// The scale of the page rendering (default 1.0)
        /// </summary>
        public double Scale { get; set; }

        public PaperSizeEnum PaperSize { get; set; }

        public PaperProperties()
        {
            MarginTop = 0.39;
            MarginBottom = 0.39;
            MarginLeft = 0.39;
            MarginRight = 0.39;
            PreferCssPageSize = false;
            PrintBackground = false;
            Landscape = false;
            Scale = 1.0;
            PaperSize = PaperSizeEnum.Letter;
        }
    }
}