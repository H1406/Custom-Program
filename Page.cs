using System;
using static SplashKitSDK.SplashKit;
public abstract class Page{
    private NavigationBar _navBar = new NavigationBar(100, 0);  
    private VerticalScrollbar _scrollBar = new VerticalScrollbar( 790, 0);
    private StockFetcher _fetcher = new StockFetcher();
 
    public Page(){
     }
    public abstract void Draw();
    
    public NavigationBar NavigationBar{
        get { return _navBar; }
    }
    public VerticalScrollbar ScrollBar{
        get { return _scrollBar; }
    }
    public StockFetcher Fetcher{
        get { return _fetcher; }
    }
}