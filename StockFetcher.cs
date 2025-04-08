using static SplashKitSDK.SplashKit;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System;
using System.Globalization;
using System.IO;

public class StockFetcher{
    private static readonly HttpClient client = new HttpClient();
    private StockItem? _itemFound = null;

    private const string ALT_KEY = "cv14fd9r01qhkk80mf70cv14fd9r01qhkk80mf7g";
    private const string API_KEY = "K6LLT7760UW0IOTE";
    private const string twelvedata_KEY = "90b6d72c568a4863ac107fdd6d9ccc1d";

    public StockFetcher(){
    }
    
    public async Task FetchStockData(string symbol)
    {
        _itemFound = null;
        string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={API_KEY}";
        try
        {
            // HttpResponseMessage response = await client.GetAsync(url);
            // response.EnsureSuccessStatusCode();
            // string responseBody = await response.Content.ReadAsStringAsync();

            // JsonDocument document = JsonDocument.Parse(responseBody);
            // JsonElement root = document.RootElement;

            // if (root.TryGetProperty("Time Series (Daily)", out JsonElement timeSeries))
            // {
            //     // Get the most recent date's data
            //     string latestDate = null;
            //     foreach (JsonProperty prop in timeSeries.EnumerateObject()) 
            //     {
            //         latestDate = prop.Name;
            //         break; // Get the first (latest) date
            //     }

            //     if (latestDate != null && timeSeries.TryGetProperty(latestDate, out JsonElement latestData))
            //     {
            //         double open = double.Parse(latestData.GetProperty("1. open").GetString(), CultureInfo.InvariantCulture);
            //         double high = double.Parse(latestData.GetProperty("2. high").GetString(), CultureInfo.InvariantCulture);
            //         double low = double.Parse(latestData.GetProperty("3. low").GetString(), CultureInfo.InvariantCulture);
            //         double price = double.Parse(latestData.GetProperty("4. close").GetString(), CultureInfo.InvariantCulture);
            //         _itemFound = new StockItem(symbol, 100, 0, high, low, open, price);
            //     }
            // }
            if(_itemFound == null){
                using(var reader = new StreamReader($"Historical Prices/{symbol}.csv")){
                    string headerLine = reader.ReadLine();
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    string PriceStr = values[1].Replace("$","");
                    double price = double.Parse(PriceStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    string OpenStr = values[3].Replace("$","");
                    double open = double.Parse(OpenStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    string HighStr = values[4].Replace("$","");
                    double high = double.Parse(HighStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    string LowStr = values[5].Replace("$","");
                    double low = double.Parse(LowStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    _itemFound = new StockItem(symbol, 100, 0, high, low, open, price);
                }
            }
        }catch (Exception)
        {
            Console.WriteLine("No data found");
        } 

    }

    

    public StockItem ItemFound{get => _itemFound;}
}