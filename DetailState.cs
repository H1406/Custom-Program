using static SplashKitSDK.SplashKit;
using System;
using Microsoft.Data.Sqlite;
using StockApp;
using SplashKitSDK;
namespace StockApp
{
    public class DetailState : IAppState
    {
        private HomePage _home;
        private DetailPage _detail;
        private Page_type _nextState;

        public DetailState(DetailPage detail,StockItem item,HomePage home){
            _detail = detail;
            _detail.Item = item;
            _home = home;
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
                }
                if (_detail.AnalyzeButton.IsClicked(MouseX(),MouseY())){
                    _detail.Analyzer.Analyze(_detail.Item.Name);
                    _detail.PredictGraph = new Graph(_detail.Item.Name,120,550,_detail.Analyzer.PredictedPrices);
                    Console.WriteLine(_detail.PredictGraph.GetLength());
                    
                }
            }
        }
        public void Update(){
        }
        public Page_type NextState{
            get=> _nextState;
        }
        public  StockItem GetItem(){
            return _detail.Item;
        }
        
    }
}