public class Converter {

    private Dictionary<string, double> _cryptoPrices = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
    
        public static void Main(string[] args) {
        // Bare et eksempel til konsollen
        Converter converter = new();
        // Indtast navn og pris for første valuta
        Console.WriteLine("Indtast navn på fra kryptovaluta:");
        string? firstCurrencyName = Console.ReadLine();

        Console.WriteLine($"Indtast prisen på en enhed af {firstCurrencyName} i USD:");
        double firstCurrencyPrice;
        while (!double.TryParse(Console.ReadLine(), out firstCurrencyPrice) || firstCurrencyPrice <= 0) {
            Console.WriteLine("Indtast en gyldig pris (større end 0):");
        }
        converter.SetPricePerUnit(firstCurrencyName, firstCurrencyPrice);

        // Indtast navn og pris for anden valuta
        Console.WriteLine("\nIndtast navn på til kryptovaluta:");
        string? secondCurrencyName = Console.ReadLine();

        Console.WriteLine($"Indtast prisen på en enhed af {secondCurrencyName} i USD:");
        double secondCurrencyPrice;
        while (!double.TryParse(Console.ReadLine(), out secondCurrencyPrice) || secondCurrencyPrice <= 0) {
            Console.WriteLine("Indtast en gyldig pris (større end 0):");
        }
        converter.SetPricePerUnit(secondCurrencyName, secondCurrencyPrice);

        // Udfør konvertering
        double result = converter.Convert(firstCurrencyName, secondCurrencyName, 1);
        Console.WriteLine($"\n1 {firstCurrencyName} er lig med {result} {secondCurrencyName}.");
    }

    /// <summary>
    /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
    /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
    /// bliver den gamle værdi overskrevet af den nye værdi
    /// </summary>
    /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
    /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
    public void SetPricePerUnit(String currencyName, double price) {
        if (price <= 0) {
                    throw new ArgumentException("Prisen kan ikke være 0 eller negativ.");
                }
                _cryptoPrices[currencyName] = price;
    }

    /// <summary>
    /// Konverterer fra en kryptovaluta til en anden. 
    /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
    /// 
    /// </summary>
    /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
    /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
    /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
    /// <returns>Værdien af beløbet i toCurrencyName</returns>
    public double Convert(String fromCurrencyName, String toCurrencyName, double amount) {
        if (!_cryptoPrices.ContainsKey(fromCurrencyName)) {
            throw new ArgumentException($"Kryptovaluta {fromCurrencyName} eksisterer ikke.");
        }
        if (!_cryptoPrices.ContainsKey(toCurrencyName)) {
            throw new ArgumentException($"Kryptovaluta {toCurrencyName} eksisterer ikke.");
        }

        double usdValue = amount * _cryptoPrices[fromCurrencyName];
        return usdValue / _cryptoPrices[toCurrencyName];
    }
}