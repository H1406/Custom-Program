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
                    _detail.PredictGraph = null;
                }
                else if (pageClicked == "follow"){
                    _nextState = Page_type.following;
                    _detail.PredictGraph = null;
                }else if (pageClicked == "wallet"){
                    _nextState = Page_type.wallet;
                    _detail.PredictGraph = null;
                }
                if (_detail.AnalyzeButton.IsClicked(MouseX(),MouseY())){
                    _detail.Analyzer.Analyze(_detail.Item.Name);
                    _detail.PredictGraph = new Graph(_detail.Item.Name,120,550,_detail.Analyzer.PredictedPrices);               
                }
                if (!_detail.IsBuy || !_detail.IsSell){
                    if (_detail.BuyButton.IsClicked(MouseX(),MouseY())){
                        _detail.IsBuy = true;
                    }
                    else if (_detail.SellButton.IsClicked(MouseX(),MouseY())){
                        _detail.IsSell = true;
                    }
                    else if(_detail.Transaction.ConfirmButton.IsClicked(MouseX(),MouseY())){
                        if (_detail.Transaction.InputTerm != ""){
                            string amount = _detail.Transaction.InputTerm;
                            if (_detail.IsBuy){
                            _wallet.Buy(_detail.Item,amount);
                            _follow.Load();
                            }else{
                                _wallet.Sell(_detail.Item,amount);
                                _follow.Load();
                            }
                        }
                        _detail.IsBuy = false;
                        _detail.IsSell = false;
                        _detail.Transaction.Deactivate();
                        _nextState = Page_type.wallet;
                        _detail.PredictGraph = null;
                    } 
                    else if(_detail.Transaction.CancelButton.IsClicked(MouseX(),MouseY())){
                        _detail.IsBuy = false;
                        _detail.IsSell = false;
                        _detail.Transaction.Deactivate();
                    }
                    else{
                        _detail.IsBuy = false;
                        _detail.IsSell = false;
                    }
                }
            }
            if (KeyTyped(KeyCode.ReturnKey)){
                if (_detail.Transaction.InputTerm != ""){
                    string amount = _detail.Transaction.InputTerm;
                    if (_detail.IsBuy){
                    _wallet.Buy(_detail.Item,amount);
                    _follow.Load();
                    }else{
                        _wallet.Sell(_detail.Item,amount);
                        _follow.Load();
                    }
                }
                _detail.IsBuy = false;
                _detail.IsSell = false;
                _detail.Transaction.Deactivate();
                _nextState = Page_type.wallet;
                _detail.PredictGraph = null;
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