using System;
using static SplashKitSDK.SplashKit;
public class StockItem:Item{
    private static int TextSize = 20;
    private static string FontName = "Arial";
    private double _high;
    private double _low;
    private double _open;
    private double _current;
    public StockItem(string symbol,float x, float y,double high, double low,double open, double current):base(symbol,x,y){
        Width = 20;
        Height = 20;
        _high = high;
        _low = low;
        _open = open;
        _current = current;
    }
    public override void Draw()
    {
        DrawText(Name,ColorWhite(),FontName,TextSize, X, Y);
        DrawText($"High: {_high}",ColorWhite(),FontName,TextSize, X+50, Y);
        DrawText($"Low: {_low}",ColorWhite(),FontName,TextSize, X+200, Y);
        DrawText($"Open: {_open}",ColorWhite(),FontName,TextSize, X+320, Y);
        DrawText($"Current: {_current}",ColorWhite(),FontName,TextSize, X+480, Y);
    }
    public double Current{
        get{
            return _current;
        }
    }
    public double High{
        get{
            return _high;
        }
    }
    public double Low{
        get{
            return _low;
        }
    }
    public double Open{
        get{
            return _open;
        }
    }
}