using static SplashKitSDK.SplashKit;
using System.Globalization;
using CsvHelper;
using SplashKitSDK;

using System;
using System.Drawing;
public class Graph : Item
{
    private List<double> prices = new List<double>();
    private IScrollBar _scrollBar;
    private double maxPrice = 0;
    private double minPrice = 0;

    public Graph(string symbol, float x , float y) : base(symbol, x, y)
    {
        LoadData();
        Height = 200;
        Width = 5;
        _scrollBar = new HorizontalScrollBar(X+360,Y-5,(int)X,(int)X+360);
        Console.WriteLine(prices.Count);
    }
    public Graph(string symbol,float x, float y , double[] predictedPrices):base(symbol,x,y){
        LoadPredictedData(predictedPrices);
        LoadData();
        Height = 200;
        Width = 5;
        _scrollBar = new HorizontalScrollBar(X+360,Y-5,(int)X,(int)X+360);
        Console.WriteLine(prices.Count);
    }
    public override void Draw()
    {
        for (int i = 0; i < prices.Count-1 ; i++)
        {
            double y1 = prices[i];
            double x1 = X+420 - i * Width-(_scrollBar.GetScrollValue()-1)*prices.Count*Width;
            double x2 = X+420 - (i+1) * Width-(_scrollBar.GetScrollValue()-1)*prices.Count*Width;
            double y2 = prices[i+1];
            if(y1 > y2){
            DrawLine(ColorGreen(), x1, Y - (y1-minPrice) / (maxPrice-minPrice) * Height, x2, Y - (y2-minPrice) / (maxPrice-minPrice) * Height);
            }else if(y1 < y2){
                DrawLine(ColorRed(), x1, Y - (y1-minPrice) / (maxPrice-minPrice) * Height, x2, Y - (y2-minPrice) / (maxPrice-minPrice) * Height);
            }else DrawLine(RGBColor(0,216,255), x1, Y - (y1-minPrice) / (maxPrice-minPrice) * Height, x2, Y - (y2-minPrice) / (maxPrice-minPrice) * Height);
        }
    }
    public void Update(){
        _scrollBar.Update();
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
    public void LoadPredictedData(double[] predictedPrices){
        prices.Clear();
        for (int i = 0; i < predictedPrices.Count(); i++)
        {
            prices.Add(predictedPrices[predictedPrices.Count()-1-i]);
        }
    }
    public int GetLength(){
        return prices.Count;
    }
    public IScrollBar ScrollBar{get=>_scrollBar;}
}
public class StockData
{
    public required string Date { get; set; }       // Matches the "Date" column
    public float Open { get; set; }        // Matches the "Open" column
    public float High { get; set; }        // Matches the "High" column
    public float Low { get; set; }         // Matches the "Low" column
    public double Close { get; set; }       // Matches the "Close" column
    public string Volume { get; set; }        // Matches the "Volume" column
}