using System;
using System.Security.Cryptography.X509Certificates;
using static SplashKitSDK.SplashKit;
using SplashKitSDK;

public class DetailPage : Page{
    private StockItem _item;
    private Graph _graph;
    public DetailPage(string title):base(title){
    }
    public override void Draw(){
        SetUp();
        ScrollBar.Draw();
        NavigationBar.Draw();
        _item.Draw();
        if (_graph != null){
            Rectangle subWindow = new Rectangle(){
                X = 120,
                Y = 100,
                Width = 400,
                Height = 400,
            };
            SetClip(subWindow);
            DrawRectangle(ColorBlack(), subWindow);
            _graph.Draw();
            ResetClip();
        }
    }
    public StockItem Item{
        get => _item;
        set{
            _item = value;
        }
    }
    public void SetUp(){
        _item.X = 150;
        _item.Y = 20;
    }
    public Graph Graph{get=> _graph; set=> _graph = value;}
}