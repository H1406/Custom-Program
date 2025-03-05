using System;
using static SplashKitSDK.SplashKit;
public class HomePage:Page{
    private StockSession _stockSession = new StockSession();
    private SearchBar searchBar = new SearchBar("Search", 350, 10);
    private StockFetcher _stockFetcher = new StockFetcher();
    private StockItem sample = new StockItem("AAPL", 100, 100, 100, 100, 100, 100);

    public HomePage(string title):base(title){
        _stockSession.AddStock(sample);
    }
    public override void Draw(){
        ScrollBar.Draw();
        NavigationBar.Draw();
        _stockSession.Draw();
        searchBar.Draw();
    }
    public void Update(){
        _stockSession.Update(ScrollBar.GetScrollValue());
    }
    public StockSession Stocks{
        get{
            return _stockSession;
        }
    }
    public SearchBar SearchBar{
        get{
            return searchBar;
        }
    }
    public StockFetcher Fetcher{get=>_stockFetcher;}

}