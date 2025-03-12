using System;
using static SplashKitSDK.SplashKit;
public abstract class Page{
    private NavigationBar _navBar = new NavigationBar(100, 0);  
    private Scrollbar _scrollBar = new Scrollbar( 790, 0);
    private StockFetcher _fetcher = new StockFetcher();
 
    public Page(){
     }
    public abstract void Draw();
    
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