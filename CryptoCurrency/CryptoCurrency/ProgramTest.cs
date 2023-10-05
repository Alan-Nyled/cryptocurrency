using Xunit;

public class ProgramTest {

    // Ækvivalensklasse: Gyldige værdier (SetPricePerUnit metoden)
    [Fact]
    public void Test_SetPricePerUnit_ValidPrice()
    {
        var converter = new Converter();
        converter.SetPricePerUnit("litecoin", 100);
        converter.SetPricePerUnit("bitcoin", 10000);
        Assert.True(converter.Convert("litecoin", "bitcoin", 1) > 0);
    }

     // Ækvivalensklasse: Ugyldige værdier (Negativ pris og 0 i SetPricePerUnit)
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Test_SetPricePerUnit_InvalidPrice_ThrowsException(double price)
    {
        var converter = new Converter();
        Assert.Throws<ArgumentException>(() => converter.SetPricePerUnit("litecoin", price));
    }
    
    // Ækvivalensklasse: Ugyldige værdier (Tom eller null valutanavn i SetPricePerUnit)
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")] 
    public void Test_SetPricePerUnit_InvalidCurrencyName_ThrowsException(string currencyName)
    {
        var converter = new Converter();
        Assert.Throws<ArgumentException>(() => converter.SetPricePerUnit(currencyName, 100));
    }


    // Ækvivalensklasse: Gyldige værdier (Convert metoden)
    [Fact]
    public void Test_Convert_ValidCurrencies()
    {
        var converter = new Converter();
        converter.SetPricePerUnit("litecoin", 100);
        converter.SetPricePerUnit("bitcoin", 10000);
        var result = converter.Convert("litecoin", "bitcoin", 1);
        Assert.Equal(0.01, result, 5);
    }

    // Ækvivalensklasse: Ugyldige værdier (Ugyldigt 'fromCurrencyName' i Convert)
    [Fact]
    public void Test_Convert_InvalidFromCurrency_ThrowsException()
    {
        var converter = new Converter();
        converter.SetPricePerUnit("bitcoin", 10000);
        Assert.Throws<ArgumentException>(() => converter.Convert("invalidCurrency", "bitcoin", 10));
    }

    // Ækvivalensklasse: Ugyldige værdier (Ugyldigt 'toCurrencyName' i Convert)
    [Fact]
    public void Test_Convert_InvalidToCurrency_ThrowsException()
    {
        var converter = new Converter();
        converter.SetPricePerUnit("bitcoin", 10000);
        Assert.Throws<ArgumentException>(() => converter.Convert("bitcoin", "invalidCurrency", 10));
    }

    // Ækvivalensklasse: Ugyldige værdier (0 antal i Convert)
    [Fact]
    public void Test_Convert_InvalidAmount_ThrowsException()
    {
        var converter = new Converter();
        converter.SetPricePerUnit("bitcoin", 10000);
        converter.SetPricePerUnit("ethereum", 500);
        var result = converter.Convert("bitcoin", "ethereum", 0);
        Assert.True(result == 0);
    }
}