using System;
using static SplashKitSDK.SplashKit;
using SplashKitSDK;
using StockApp;
namespace StockApp;
// Concrete states
public class HomeState : IAppState
{
    private HomePage _home;
    private FollowPage _follow;
    private Page_type _nextState;
    private StockItem _itemSearched;
    private DetailPage _detail;
    
    // private Analyzer analyzer = new Analyzer();

    public HomeState(HomePage home, FollowPage follow, DetailPage detail)
    {
        _home = home;
        _follow = follow;
        _nextState = Page_type.home; // Default to staying in the same state
        _detail = detail;
    }

    public void HandleInput()
    {
        if (MouseClicked(MouseButton.LeftButton))
        {
            _home.ScrollBar.IsClicked(MouseX(), MouseY());
            _home.SearchBar.IsClicked(MouseX(), MouseY());

            StockItem itemAdded = _home.Stocks.Buttons.StockAdd(MouseX(), MouseY());
            StockItem itemDeleted = _home.Stocks.Buttons.StockDelete(MouseX(), MouseY());
            if (itemAdded != null)
            {
                StockItem stock = new StockItem(itemAdded.Name, itemAdded.X, itemAdded.Y, itemAdded.High, itemAdded.Low, itemAdded.Open, itemAdded.Current);
                _follow.Stocks.AddStock(stock);
                _follow.SaveStock(stock);
            }
            if (itemDeleted != null)
            {
                _home.Stocks.RemoveStock(itemDeleted);
            }
            string pageClicked = _home.NavigationBar.PageClicked();
            if (pageClicked == "follow")
            {
                _nextState = Page_type.following;
            }
            else if (pageClicked == "analysis")
            {
                _nextState = Page_type.analysis;
            }
            _itemSearched = _home.Stocks.StockClicked(MouseX(), MouseY());
            if (ItemSearched != null)
            {
                _detail.Graph = new Graph(_itemSearched.Name);
                _detail.Graph.LoadData();
                _nextState = Page_type.detail;
            }
        }

        if (KeyTyped(KeyCode.ReturnKey))
        {
            string searchTerm = _home.SearchBar.SearchTerm;
            if (searchTerm != null)
            {
                _home.Fetcher.FetchStockData(searchTerm).Wait();
                _itemSearched = _home.Fetcher.ItemFound;
                _home.Stocks.AddStock(_itemSearched);
            }
            // Console.WriteLine(Analyzer.GetBotResponseAsync("Hello"));
        }
    }

    public void Draw()
    {
        _home.Draw();
    }
    public void Update()
    {
        _home.Update();
    }
    public Page_type NextState => _nextState;
    public string SearchTerm => _home.SearchBar.SearchTerm;
    public StockItem ItemSearched => _itemSearched;
    public StockItem GetItem(){
        return _itemSearched;
    }

}