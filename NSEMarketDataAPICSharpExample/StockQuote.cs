using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NSEMarketDataAPICSharpExample
{
    class StockQuote
    {
        public string Symbol { set; get; }
        public double Price { set; get; }
        public DateTime LastTradeTime { set; get; }

        //define members you are interested in here
    }

    class StockQuoteClient
    {
        public async void FetchUntypedWith(string nseBaseURI, string myUserName, string myToken)
        {
            var settings = new ODataClientSettings(nseBaseURI);
            settings.BeforeRequest = (message) =>
            {
                String aux = String.Join(":", new String[] { myUserName, myToken });
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(aux);
                message.Headers.TryAddWithoutValidation("Authorization",
                "Basic " + System.Convert.ToBase64String(plainTextBytes));
            };

            var client = new ODataClient(settings);
            var stockQuote = await client
                .For("StockQuote")
                .Key("FBNH")
                .FindEntryAsync();

            //You can store retrieved data for later use by your client.

            Console.WriteLine(DateTime.Now.ToString() + ": " + stockQuote["Symbol"] + ": " + stockQuote["Price"] + " as @ " + stockQuote["LastTradeTime"]);
        }

        public async void FetchTypedWith(string nseBaseURI, string myUserName, string myToken)
        {
            var settings = new ODataClientSettings(nseBaseURI);
            settings.BeforeRequest = (message) =>
            {
                String aux = String.Join(":", new String[] { myUserName, myToken });
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(aux);
                message.Headers.TryAddWithoutValidation("Authorization",
                "Basic " + System.Convert.ToBase64String(plainTextBytes));
            };

            var client = new ODataClient(settings);
            var stockQuote = await client
                .For<StockQuote>()
                .Key("FBNH")
                .FindEntryAsync();

            //You can store retrieved data for later use by your client.

            Console.WriteLine(DateTime.Now.ToString() + ": "+ stockQuote.Symbol + ": " + stockQuote.Price.ToString("N02") + " as @ " + stockQuote.LastTradeTime.ToString());
        }
    }
}
