using System.ComponentModel;

//namespace DossierPortailAPI.Services.Gotenberg.Ressources
namespace ApiTestMongo.Gotenberg.Resources
{
    public enum PaperSizeEnum
    {
        [Description("8.5x11")]
        Letter,

        [Description("8.5x14")]
        Legal,

        [Description("11x17")]
        Tabloid,

        [Description("17x11")]
        Ledger,

        [Description("33.1x46.8")]
        A0,

        [Description("23.4x33.1")]
        A1,

        [Description("16.54x23.4")]
        A2,

        [Description("11.7x16.54")]
        A3,

        [Description("8.27x11.7")]
        A4,

        [Description("5.83x8.27")]
        A5,

        [Description("4.13x5.83")]
        A6,

        Custom
    }
}