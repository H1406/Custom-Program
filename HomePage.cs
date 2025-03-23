using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
public class HomePage:Page{
    private StockSession _stockSession = new StockSession();
    private TextInput _searchBar = new TextInput( 350, 10,200,22);
    private bool _isLoading = false;
    private StockItem sample = new StockItem("AAPL", 100, 100, 100, 100, 100, 100);
    private string[] topStocks = ["AAPL","GOOG","TSLA","AMZN","EBAY","NVDA","META"];
    private static HomePage _instance;
    private static readonly object _lock = new object();

    private HomePage(){
        // Load();
        Stocks.AddStock(sample, 52);
    }
    public static HomePage GetInstance(){
        if (_instance == null){
            lock (_lock){
                if (_instance == null){
                    _instance = new HomePage();
                }
            }
        }
        return _instance;
    }
    public override void Draw(){
        ScrollBar.Draw();
        NavigationBar.Draw();
        if (_isLoading){
            DrawText("Loading...",ColorWhite(),"Arial",100, 400, 250);
        }else{
            Rectangle subWindow = new Rectangle(){
                X = 110,
                Y = 60,
                Width = 680,
                Height = 500,
            };
            SetClip(subWindow);
            DrawRectangle(ColorBlack(), subWindow);
            _stockSession.Draw();
            ResetClip();
        }
        _searchBar.Draw();
    }
    public override void Update(){
        _stockSession.Update(ScrollBar.GetScrollValue());
        ScrollBar.Update();
    }
    public async void Load(){
        _isLoading = true;
        foreach (string stock in topStocks){
            await Fetcher.FetchStockData(stock);
            Stocks.AddStock(Fetcher.ItemFound, 0);
        }
        _isLoading = false;
    }
    public StockSession Stocks{
        get{
            return _stockSession;
        }
    }
    public TextInput SearchBar{
        get{
            return _searchBar;
        }
    }

}