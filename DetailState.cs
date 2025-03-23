using static SplashKitSDK.SplashKit;
using System;
using Microsoft.Data.Sqlite;
using StockApp;
using SplashKitSDK;
namespace StockApp
{
    public class DetailState : IAppState
    {
        private FollowPage _follow;
        private DetailPage _detail;
        private Page_type _nextState;
        private WalletPage _wallet;

        public DetailState(DetailPage detail,StockItem item,FollowPage follow,WalletPage wallet){
            _detail = detail;
            _detail.Item = item;
            _follow = follow;
            _wallet = wallet;   
            _nextState = Page_type.detail; 
        }
        public void Draw(){
            _detail.Draw();
        }
        public void HandleInput(){
            if (MouseClicked(MouseButton.LeftButton)){
                string pageClicked = _detail.NavigationBar.PageClicked();
                if (pageClicked == "home"){
                    _nextState = Page_type.home;
                }
                else if (pageClicked == "follow"){
                    _nextState = Page_type.following;
                }else if (pageClicked == "wallet"){
                    _nextState = Page_type.wallet;
                }
                if (_detail.AnalyzeButton.IsClicked(MouseX(),MouseY())){
                    _detail.Analyzer.Analyze(_detail.Item.Name);
                    _detail.PredictGraph = new Graph(_detail.Item.Name,120,550,_detail.Analyzer.PredictedPrices);                    
                }
                if (!_detail.IsBuy || !_detail.IsSell){
                    if (_detail.BuyButton.IsClicked(MouseX(),MouseY())){
                        _detail.IsBuy = true;
                    }
                    if (_detail.SellButton.IsClicked(MouseX(),MouseY())){
                        _detail.IsSell = true;
                    }
                }
                if(_detail.Transaction.ConfirmButton.IsClicked(MouseX(),MouseY()) && _detail.IsBuy){
                    string amount = "0";
                    if (_detail.Transaction.InputTerm != ""){
                        amount = _detail.Transaction.InputTerm;
                        EndReadingText();
                    }
                    Console.WriteLine(_wallet.Wallet.Total);
                    Console.WriteLine(amount);
                    if (_detail.IsBuy){
                        _wallet.Wallet.Buy(_detail.Item,amount);
                    }else{
                        _wallet.Wallet.Sell(_detail.Item,amount);
                    }
                    _detail.IsBuy = false;
                    _detail.IsSell = false;
                    Console.WriteLine(_wallet.Wallet.Total);
                    _nextState = Page_type.wallet;
                } 
            }
            if (KeyTyped(KeyCode.ReturnKey)){
                string amount = "0";
                if (_detail.Transaction.InputTerm != ""){
                    amount = _detail.Transaction.InputTerm;
                    _detail.Transaction.Deactivate();
                }
                Console.WriteLine(_wallet.Wallet.Total);
                Console.WriteLine(amount);
                if (_detail.IsBuy){
                    _wallet.Wallet.Buy(_detail.Item,amount);
                    _follow.SaveStock(_detail.Item);
                    _follow.Stocks.AddStock(_detail.Item,50);
                }else{
                    _wallet.Wallet.Sell(_detail.Item,amount);
                }
                _detail.IsBuy = false;
                _detail.IsSell = false;
                Console.WriteLine(_wallet.Wallet.Total);
                _nextState = Page_type.wallet;
            }
        }
        public void Update(){
            _detail.Update();
            if(_detail.IsBuy || _detail.IsSell){
                _detail.Transaction.Activate();
            }else _detail.Transaction.Deactivate();
        }
        public Page_type NextState{
            get=> _nextState;
        }
        public  StockItem GetItem(){
            return _detail.Item;
        }
        
    }
}