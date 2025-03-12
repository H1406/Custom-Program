using static SplashKitSDK.SplashKit;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Data.Sqlite;
public class FollowPage : Page{
    private DatabaseManager _manager = new DatabaseManager();
    private StockSession _stocks = new StockSession();

    public FollowPage(){
        Load();
    }
    public void Load(){
        _manager.LoadData().Wait();
        _stocks.LoadStocks(_manager.Stocks);
    }

    public override void Draw(){
        Update();
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
        _manager.SaveStock(stock);
    }
    public void Update(){
        _stocks.Update(ScrollBar.GetScrollValue());
    }
    
    public StockSession Stocks{get=>_stocks;}
    public DatabaseManager Manager{get=>_manager;}

}