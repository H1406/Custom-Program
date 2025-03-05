using static SplashKitSDK.SplashKit;
using System;

public class StockGraph
{
    private double[] _prices;
    private float StartX = 150;
    private float StartY = 200;
    private int Width = 20;
    private int Height = 300;

    public StockGraph(double[] prices)
    {
        _prices = prices;
    }

    public void Draw()
    {
        for (int i = 0; i < _prices.Length; i++)
        {
            DrawRectangle(ColorWhite(), StartX + i*20, StartY+(_prices[i]/_prices.Max()*Height), Width, _prices[i]/_prices.Max()*Height);
        }
    }
}