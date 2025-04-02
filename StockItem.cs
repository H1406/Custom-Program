using System;
using static SplashKitSDK.SplashKit;
public class StockItem:Item{
    private static int TextSize = 20;
    private static string FontName = "Arial";
    private double _high;
    private double _low;
    private double _open;
    private double _current;
    private int _quantity = 0;
    public StockItem(string symbol,float x, float y,double high, double low,double open, double current):base(symbol,x,y){
        Width = 680;
        Height = 50;
        _high = high;
        _low = low;
        _open = open;
        _current = current;
    }
    public StockItem(string symbol,float x, float y,double high, double low,double open, double current,int quantity):base(symbol,x,y){
        Width = 680;
        Height = 50;
        _high = high;
        _low = low;
        _open = open;
        _current = current;
        _quantity = quantity;
    }
    public override void Draw()
    {
        if (IsHovered()){
            FillRectangle(RGBColor(128,128,128), X, Y, Width+10, Height);
        }
        DrawText(Name,ColorWhite(),FontName,TextSize, X+20, Y+20);
        DrawText($"High: {_high}",ColorWhite(),FontName,TextSize, X+70, Y+20);
        DrawText($"Low: {_low}",ColorWhite(),FontName,TextSize, X+220, Y+20);
        DrawText($"Open: {_open}",ColorWhite(),FontName,TextSize, X+340, Y+20);
        DrawText($"Current: {_current}",ColorWhite(),FontName,TextSize, X+500, Y+20);
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
    public int Quantity{
        get{
            return _quantity;
        }
        set{
            _quantity = value;
        }
    }
}