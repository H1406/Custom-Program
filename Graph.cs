using static SplashKitSDK.SplashKit;
using System.Globalization;
using CsvHelper;

using System;
using System.Drawing;
public class Graph : Item
{
    private List<float> prices = new List<float>();
    private float maxPrice = 0;
    private float minPrice = 0;
    public int Height { get; set; }

    public Graph(string symbol, float x = 120, float y = 500) : base(symbol, x, y)
    {
        Height = 200;
        Width = 20;
    }

    public override void Draw()
    {
        for (int i = 0; i < prices.Count; i++)
        {
            // Draw each point of the graph
            FillRectangle(ColorBlue(), X + i * Width, Y - (prices[i]-minPrice) / maxPrice * Height, Width, prices[i] / maxPrice * Height);
            DrawRectangle(ColorBlack(), X + i * Width, Y - (prices[i]-minPrice) / maxPrice * Height, Width, prices[i] / maxPrice * Height);
        }
    }

    public void LoadData()
    {
        using (var reader = new StreamReader($"Historical Prices/{Name}.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Read the CSV data into a list of StockData objects
            var records = csv.GetRecords<StockData>().ToList();

            // Extract the "Close" prices and store them in the prices list
            foreach (var record in records)
            {
                prices.Add(record.Close);
            }

            // Calculate the maximum price for scaling the graph
            maxPrice = prices.Max();
            minPrice = prices.Min();
        }
    }
}
public class StockData
{
    public string Date { get; set; }       // Matches the "Date" column
    public float Open { get; set; }        // Matches the "Open" column
    public float High { get; set; }        // Matches the "High" column
    public float Low { get; set; }         // Matches the "Low" column
    public float Close { get; set; }       // Matches the "Close" column
    public string Volume { get; set; }        // Matches the "Volume" column
}