using GeoFlat.Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoFlat.Server.Tests
{
    public class CurrencyConverterTests
    {
        [Fact]
        public void ConvertFromUSDToBYNPriceUSDCorrectResult()
        {
            //Arrange
            CurrencyConverter converter = new CurrencyConverter
            {
                USD = 2,
                BYN = 5
            };
            int ParamPriceUSD = 10;
            int exptectedResultPrice = 4;
            //Act
            int result = converter.ConvertFromUSDToBYN(ParamPriceUSD);

            //Assert          
            Assert.Equal(exptectedResultPrice, result);
        }
    }
}
