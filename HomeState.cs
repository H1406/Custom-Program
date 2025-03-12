using System;
using static SplashKitSDK.SplashKit;
using SplashKitSDK;
using StockApp;
namespace StockApp;
// Concrete states
public class HomeState(HomePage home, FollowPage follow, DetailPage detail) : IAppState
{
    private HomePage _home = home;
    private FollowPage _follow = follow;
    private Page_type _nextState = Page_type.home;
    private StockItem? _itemSearched;
    private DetailPage _detail = detail;

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
                _follow.Stocks.AddStock(stock,100);
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
                _detail.Graph = new Graph(_itemSearched.Name,120,300);
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
                _home.Stocks.AddStock(_itemSearched,100);
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