using System;
using static SplashKitSDK.SplashKit;
public class HomePage:Page{
    private StockSession _stockSession = new StockSession();
    private SearchBar _searchBar = new SearchBar( 350, 10);
    private bool _isLoading = false;
    private StockItem sample = new StockItem("AAPL", 100, 100, 100, 100, 100, 100);
    private string[] topStocks = ["AAPL","GOOG","TSLA","AMZN","EBAY","NVDA","META"];

    public HomePage(){
        // Load();
        Stocks.AddStock(sample, 70);
    }
    public override void Draw(){
        ScrollBar.Draw();
        NavigationBar.Draw();
        if (_isLoading){
            DrawText("Loading...",ColorWhite(),"Arial",100, 400, 250);
        }else _stockSession.Draw();
        _searchBar.Draw();
    }
    public void Update(){
        _stockSession.Update(ScrollBar.GetScrollValue());
    }
    public async void Load(){
        _isLoading = true;
        foreach (string stock in topStocks){
            await Fetcher.FetchStockData(stock);
            Stocks.AddStock(Fetcher.ItemFound, 70);
        }
        _isLoading = false;
    }
    public StockSession Stocks{
        get{
            return _stockSession;
        }
    }
    public SearchBar SearchBar{
        get{
            return _searchBar;
        }
    }

}