namespace GeoFlat.Server.Helpers
{
    public class CurrencyConverter
    {
        public decimal USD { get; set; }
        public decimal BYN { get; set; }
        public int ConvertFromUSDToBYN(decimal priceUSD)
       => System.Convert.ToInt32((USD * priceUSD) / BYN);
    }
}
