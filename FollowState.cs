using static SplashKitSDK.SplashKit;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using StockApp;
using SplashKitSDK;
namespace StockApp
{
    public class FollowState : IAppState{
        private FollowPage _follow;
        private HomePage _home;
        private DetailPage _detail; 
        private Page_type _nextState;
        private StockItem _itemSearched;

        public FollowState(FollowPage follow, HomePage home, DetailPage detail)
        {
            _follow = follow;
            _home = home;
            _nextState = Page_type.following; // Default to staying in the same state
            _detail = detail;
        }

        public void HandleInput()
        {
            if (MouseClicked(MouseButton.LeftButton))
            {
                string pageClicked = _follow.NavigationBar.PageClicked();
                if (pageClicked == "home")
                {
                    _nextState = Page_type.home;
                }
                else if (pageClicked == "wallet")
                {
                    _nextState = Page_type.wallet;
                }

                _itemSearched = _follow.Stocks.StockClicked(MouseX(), MouseY());
                if (_itemSearched != null)
                {
                    _detail.Graph = new Graph(_itemSearched.Name,120,300);
                    _nextState = Page_type.detail;
                }
                StockItem itemDeleted = _follow.Stocks.Buttons.StockDelete(MouseX(), MouseY());
                if (itemDeleted != null)
                {
                    _follow.Stocks.RemoveStock(itemDeleted);
                    _follow.Manager.RemoveStock(itemDeleted);
                    _nextState = Page_type.following;
                }   
                
            }
        }

        public void Draw()
        {
            _follow.Draw();
        }
        public void Update()            
        {
            _follow.Update();
        }
        public Page_type NextState => _nextState;
        public StockItem GetItem(){
            return _itemSearched;
        }

    }
}