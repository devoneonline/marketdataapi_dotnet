using System;
using System.Threading;
using System.Threading.Tasks;

namespace NSEMarketDataAPICSharpExample
{
    class Program
    {
        //get this from NSE Market Data API website
        readonly static string NSE_BASEURI = "http://marketdataapi.nse.com.ng/v1/odata/";
        
        //issued to you by NSE on completion of onboarding
        readonly static string USER_NAME = "tester";
        readonly static string USER_TOKEN = "6xxxxd01-8xx0-4468-9xxx9-453959f7xxxx";

        static void Main(string[] args)
        {
            while (true)
            {
                var stockQuoteClient = Task.Run(() => new StockQuoteClient().FetchTypedWith(NSE_BASEURI, USER_NAME, USER_TOKEN));
                
                // Output a message from the calling thread.
                Console.WriteLine(DateTime.Now.ToString() + ": StockQuote data request sent to NSE");
                stockQuoteClient.Wait();

                Thread.Sleep(60000);
            }
        }
    }
}
