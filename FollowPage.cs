using static SplashKitSDK.SplashKit;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using SplashKitSDK;
public class FollowPage : Page{
    private DatabaseManager _manager = new DatabaseManager();
    private StockSession _stocks = new StockSession();
    private static FollowPage _instance;
    private static object _lock = new object();

    public static FollowPage GetInstance(){
        if (_instance == null){
            lock (_lock){
                if (_instance == null){
                    _instance = new FollowPage();
                }
            }
        }
        return _instance;
    }

    public FollowPage(){
        // Load();
    }
    public void Load(){
        _manager.LoadData().Wait();
        _stocks.LoadStocks(_manager.Stocks);
    }

    public override void Draw(){
        DrawText("Following Stocks",ColorWhite(),"Arial",50, 400, 20);
        Rectangle subWindow = new Rectangle(){
            X = 110,
            Y = 40,
            Width = 680,
            Height = 500,
        };
        SetClip(subWindow);
        DrawRectangle(ColorBlack(), subWindow);
         _stocks.Draw();
        ResetClip();
        NavigationBar.Draw();
        ScrollBar.Draw();
    }
    public void SaveStock(StockItem stock){
        foreach (StockItem _stock in _stocks.Stocks){
            if (_stock.Name == stock.Name){
                Console.WriteLine($"Stock {stock.Name} already exists!");
                return;
            }
        }
        Console.WriteLine($"Stock {stock.Name} saved successfully!");
        _manager.SaveStock(stock);
    }
    public override void Update(){
        ScrollBar.Update();
        _stocks.Update(ScrollBar.GetScrollValue());
    }
    
    public StockSession Stocks{get=>_stocks;}
    public DatabaseManager Manager{get=>_manager;}

}