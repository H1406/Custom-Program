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
            _home.SearchBar.IsClicked(MouseX(), MouseY());

            StockItem itemAdded = _home.Stocks.Buttons.StockAdd(MouseX(), MouseY());
            StockItem itemDeleted = _home.Stocks.Buttons.StockDelete(MouseX(), MouseY());
            if (itemAdded != null)
            {
                StockItem stock = new StockItem(itemAdded.Name, itemAdded.X, itemAdded.Y, itemAdded.High, itemAdded.Low, itemAdded.Open, itemAdded.Current);
                _follow.SaveStock(stock);
                _follow.Stocks.AddStock(stock,50);
                Console.WriteLine($"Stock {stock.Name} added to following!");
                Console.WriteLine($"Stock {stock.Name} saved successfully!");
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
            else if (pageClicked == "wallet")
            {
                _nextState = Page_type.wallet;
            }
            _itemSearched = _home.Stocks.StockClicked(MouseX(), MouseY());
            if (ItemSearched != null)
            {
                foreach (StockItem stock in _follow.Stocks.Stocks)
                {
                    if (stock.Name == _itemSearched.Name)
                    {
                        _itemSearched.Quantity = stock.Quantity;
                        break;
                    }
                }
                _detail.Graph = new Graph(_itemSearched.Name,120,300);
                _nextState = Page_type.detail;
            }
        }

        if (KeyTyped(KeyCode.ReturnKey))
        {
            string searchTerm = _home.SearchBar.InputTerm;
            if (searchTerm != null)
            {
                _itemSearched = _home.Manager.LoadStock(searchTerm);
                if(_itemSearched == null){
                    FadeMusicIn(new Music("invalid","invalid.mp3"),1000);
                }else _home.Stocks.AddStock(_itemSearched,52);
            }
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
    public string SearchTerm => _home.SearchBar.InputTerm;
    public StockItem ItemSearched => _itemSearched;
    public StockItem GetItem(){
        return _itemSearched;
    }

}