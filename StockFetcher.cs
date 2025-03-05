using static SplashKitSDK.SplashKit;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System;
using System.Globalization;

public class StockFetcher{
    private static readonly HttpClient client = new HttpClient();
    private StockItem _itemFound = null;

    private const string API_KEY = "cv14fd9r01qhkk80mf70cv14fd9r01qhkk80mf7g";
    private const string ALT_API_KEY = "69PD823FYSW14L8U";

    public StockFetcher(){
    }
    // public async Task FetchStockData(string symbol)
    // {
    //     string url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={API_KEY}";
    //     string alt_url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={ALT_API_KEY}";
    //     try
    //     {
    //         HttpResponseMessage response = await client.GetAsync(url);
    //         response.EnsureSuccessStatusCode();
    //         string responseBody = await response.Content.ReadAsStringAsync();

    //         JsonDocument document = JsonDocument.Parse(responseBody);
    //         JsonElement root = document.RootElement;

    //         double price = root.GetProperty("c").GetDouble();
    //         double high = root.GetProperty("h").GetDouble();
    //         double low = root.GetProperty("l").GetDouble();
    //         double open = root.GetProperty("o").GetDouble();
    //         _itemFound = new StockItem(symbol, 100, 0, high, low, open, price);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Error fetching stock data: {ex.Message}");
    //         HttpResponseMessage response = await client.GetAsync(alt_url);
    //         response.EnsureSuccessStatusCode();
    //         string responseBody = await response.Content.ReadAsStringAsync();

    //         // Print response to check the actual structure
    //         Console.WriteLine(responseBody);

    //         JsonDocument document = JsonDocument.Parse(responseBody);
    //         JsonElement root = document.RootElement;

    //         if (root.TryGetProperty("Time Series (Daily)", out JsonElement timeSeries))
    //         {
    //             // Get the most recent date's data
    //             string latestDate = null;
    //             foreach (JsonProperty prop in timeSeries.EnumerateObject()) 
    //             {
    //                 latestDate = prop.Name;
    //                 break; // Get the first (latest) date
    //             }

    //             if (latestDate != null && timeSeries.TryGetProperty(latestDate, out JsonElement latestData))
    //             {
    //                 double open = double.Parse(latestData.GetProperty("1. open").GetString(), CultureInfo.InvariantCulture);
    //                 double high = double.Parse(latestData.GetProperty("2. high").GetString(), CultureInfo.InvariantCulture);
    //                 double low = double.Parse(latestData.GetProperty("3. low").GetString(), CultureInfo.InvariantCulture);
    //                 double price = double.Parse(latestData.GetProperty("4. close").GetString(), CultureInfo.InvariantCulture);

    //                 _itemFound = new StockItem(symbol, 100, 0, high, low, open, price);
    //             }
    //             else
    //             {
    //                 Console.WriteLine("Error: No data found for latest date.");
    //             }
    //         }
    //         else
    //         {
    //             Console.WriteLine("Error: 'Time Series (Daily)' key not found in the response.");
    //         }
    //     }
    // }
    public async Task FetchStockData(string symbol)
    {
        string alt_url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={ALT_API_KEY}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(alt_url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonDocument document = JsonDocument.Parse(responseBody);
            JsonElement root = document.RootElement;

            if (root.TryGetProperty("Time Series (Daily)", out JsonElement timeSeries))
            {
                // Get the most recent date's data
                string latestDate = null;
                foreach (JsonProperty prop in timeSeries.EnumerateObject()) 
                {
                    latestDate = prop.Name;
                    break; // Get the first (latest) date
                }

                if (latestDate != null && timeSeries.TryGetProperty(latestDate, out JsonElement latestData))
                {
                    double open = double.Parse(latestData.GetProperty("1. open").GetString(), CultureInfo.InvariantCulture);
                    double high = double.Parse(latestData.GetProperty("2. high").GetString(), CultureInfo.InvariantCulture);
                    double low = double.Parse(latestData.GetProperty("3. low").GetString(), CultureInfo.InvariantCulture);
                    double price = double.Parse(latestData.GetProperty("4. close").GetString(), CultureInfo.InvariantCulture);
                    _itemFound = new StockItem(symbol, 100, 0, high, low, open, price);
                }
            }
        }catch (Exception ex)
        {
            Console.WriteLine($"Error fetching stock data: {ex.Message}");
        }
        
    }

    

    public StockItem ItemFound{get => _itemFound;}
}