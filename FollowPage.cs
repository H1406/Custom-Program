using static SplashKitSDK.SplashKit;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Data.Sqlite;
public class FollowPage : Page{
    private DatabaseManager manager = new DatabaseManager();
    private StockSession _stocks = new StockSession();

    public FollowPage(string title) : base(title){
        Load();
    }
    public void Load(){
        manager.LoadData().Wait();
        Console.WriteLine(manager.Stocks.Count());
        _stocks.LoadStocks(manager.Stocks);
    }

    public override void Draw(){
        NavigationBar.Draw();
        ScrollBar.Draw();
        _stocks.Draw();
    }
    public void SaveStock(StockItem stock){
        foreach (StockItem _stock in _stocks.Stocks){
            if (_stock.Name == stock.Name){
                return;
            }
        }
        manager.SaveStock(stock);
    }
    
    public StockSession Stocks{get=>_stocks;}
    public DatabaseManager Manager{get=>manager;}

}