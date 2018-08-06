using AngleSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoBot.Models.Parse
{
    public class CoinsInfo
    {

        private List<Coin> _coins;
        private DateTime _refreshDate;

        private CoinsInfo()
        {
        }

        static CoinsInfo()
        {
            Instance = new CoinsInfo
            {
                _coins = new List<Coin>()
            };
        }
        public static CoinsInfo Instance { get; }


        public async Task RefreshRateAsync(string[] coinsNames = null)
        {
            while (true)
            {
                if (coinsNames == null)
                    await ParseCoinsAsync();
                else
                    await ParseCoinsAsync(coinsNames);

                _refreshDate = DateTime.UtcNow.AddHours(2);
                Thread.Sleep(30000);
            }
        }

        public string CreateMessage()
        {
            var header = "📈 Курс валют на " + _refreshDate.ToString("f", CultureInfo.GetCultureInfo("ru-ru")) + "\n\n";
            var message = new StringBuilder(header);

            foreach (var coin in _coins)
                message.AppendLine(coin.GetCoinInfo());

            return message.ToString();
        }


        private async Task ParseCoinsAsync(string url = "https://coinmarketcap.com/")
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(url);
            var rows = document.QuerySelectorAll("table#currencies tbody tr");
            _coins.Clear();

            foreach (var row in rows)
            {
                var coinName = row.QuerySelector(".currency-name-container").InnerHtml;
                var coinSymbol = row.QuerySelector(".currency-symbol a").InnerHtml;
                var coinPrice = StringToFloat(row.QuerySelector(".price").GetAttribute("data-usd"));
                var coinChange = StringToFloat(row.QuerySelector(".percent-24h").GetAttribute("data-usd"));

                _coins.Add(new Coin(coinName, coinSymbol, coinPrice, coinChange));
                if (_coins.Count > 10)
                    break;
            }
            


            float StringToFloat(string value)
            {
                //if (value.Contains("."))
                    //value = value.Replace(".", ",");
                return Convert.ToSingle(value);
            }


        }

        private async Task ParseCoinsAsync(IEnumerable<string> coinsNames, string url = "https://coinmarketcap.com/")
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(url);
            var tbody = document.QuerySelector("table#currencies tbody");
            _coins.Clear();

            //var documentCap = await BrowsingContext.New(config).OpenAsync("https://coincodex.com/");
            //cap = Convert.ToUInt64(documentCap.QuerySelector("ul.market-overview span.currencyUSD"));

            foreach (var name in coinsNames)
            {
                var row = tbody.QuerySelector("#id-" + name);
                var coinName = row.QuerySelector(".currency-name-container").InnerHtml;
                var coinSymbol = row.QuerySelector(".currency-symbol a").InnerHtml;
                var coinPrice = StringToFloat(row.QuerySelector(".price").GetAttribute("data-usd"));
                var percentChange = row.QuerySelector(".percent-change").InnerHtml;
                var coinChange = StringToFloat(percentChange.Substring(0, percentChange.Length - 1));

                _coins.Add(new Coin(coinName, coinSymbol, coinPrice, coinChange));
            }

            float StringToFloat(string value)
            {
                //if (value.Contains("."))
                    //value = value.Replace(".", ",");
                return Convert.ToSingle(value);
            }

        }

    }
}