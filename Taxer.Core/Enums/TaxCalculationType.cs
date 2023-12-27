using System.ComponentModel;

namespace Taxer.Core
{
    public enum TaxCalculationType
    {
        None,
        [Description("Progressive")]
        Progressive,
        [Description("Flat Value")]
        FlatValue,
        [Description("Flat Rate")]
        FlatRate
    }
}
