using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NSEMarketDataAPICSharpExample
{
    class DelayedQuote
    {
        public string Symbol { set; get; }
        public double Last { set; get; }
        public DateTime LastTradeTime { set; get; }

        //define members you are interested in here
    }

    class DelayedQuoteClient
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
            var quote = await client
                .For("DelayedQuote")
                .Key("FBNH")
                .FindEntryAsync();

            //You can store retrieved data for later use by your client.

            Console.WriteLine(DateTime.Now.ToString() + ": " + quote["Symbol"] + ": " + quote["Last"] + " as @ " + quote["LastTradeTime"]);
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
            var quote = await client
                .For<DelayedQuote>()
                .Key("FBNH")
                .FindEntryAsync();

            //You can store retrieved data for later use by your client.

            Console.WriteLine(DateTime.Now.ToString() + ": "+ quote.Symbol + ": " + quote.Last.ToString("N02") + " as @ " + quote.LastTradeTime.ToString());
        }
    }
}
