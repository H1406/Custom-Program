using System;
using static SplashKitSDK.SplashKit;
public abstract class Page{
    private NavigationBar _navBar = new NavigationBar(100, 0);  
    private VerticalScrollbar _scrollBar = new VerticalScrollbar( 790, 0);
    private DatabaseManager _manager = new DatabaseManager();
 
    public Page(){
     }
    public abstract void Draw();
    public abstract void Update();
    
    public NavigationBar NavigationBar{
        get { return _navBar; }
    }
    public VerticalScrollbar ScrollBar{
        get { return _scrollBar; }
    }
    public DatabaseManager Manager{
        get { return _manager; }
    }
}