using System.Globalization;

namespace CryptoBot.Models.Parse
{
    public class Coin
    {
        private readonly string _name;
        private readonly string _symbol;
        private readonly float _price;
        private readonly float _change;

        public Coin(string name, string symbol, float price, float change)
        {
            _name = name;
            _symbol = symbol;
            _price = price;
            _change = change;
        }

        public string GetCoinInfo()
        {
            var linkedSymbol = $"<a href=\"https://coinmarketcap.com/currencies/{_name}\">{_symbol}</a>";
            return linkedSymbol + ": $" +
                   _price.ToString(CultureInfo.InvariantCulture).Replace(".",",") + 
                   " (" + _change.ToString(CultureInfo.InvariantCulture).Replace(".", ",") + "%)";
        }
    }
}