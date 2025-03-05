using System;
using static SplashKitSDK.SplashKit;
public abstract class Page{
    private NavigationBar _navBar = new NavigationBar("navigation bar", 100, 0);  
    private Scrollbar _scrollBar = new Scrollbar("scroll bar", 790, 0);
    private StockFetcher _fetcher = new StockFetcher();
    private string _title;
    public Page(string title){
        _title = title;
     }
    public abstract void Draw();
    public string Title{
        get { return _title; }
    }
    public NavigationBar NavigationBar{
        get { return _navBar; }
    }
    public Scrollbar ScrollBar{
        get { return _scrollBar; }
    }
    public StockFetcher Fetcher{
        get { return _fetcher; }
    }
}