using StockApp;
using static SplashKitSDK.SplashKit;
using SplashKitSDK;

public class WalletState(WalletPage wallet):IAppState{
    private WalletPage _wallet = wallet;
    private StockItem? itemClicked = null;
    private Page_type _nextState = Page_type.wallet;
    public void HandleInput(){
        if (MouseClicked(MouseButton.LeftButton)){
            itemClicked = _wallet.Stocks.StockClicked(MouseX(),MouseY());
            if(itemClicked != null) _nextState = Page_type.detail;
            if(_wallet.Deposit.IsClicked(MouseX(),MouseY())) _wallet.Withdraw.IsSelected = false;
            if(_wallet.Withdraw.IsClicked(MouseX(),MouseY())) _wallet.Deposit.IsSelected = false;
            string pageClicked = _wallet.NavigationBar.PageClicked();
            if (pageClicked == "home"){
                _nextState = Page_type.home;
            }
            else if (pageClicked == "follow"){
                _nextState = Page_type.following;
            }
        }
        if (KeyTyped(KeyCode.ReturnKey)){
            string searchTerm = _wallet.InputTerm;
            if (searchTerm != null){
                if (_wallet.Deposit.IsSelected){
                    _wallet.Wallet.Deposit(searchTerm);
                    _wallet.Deposit.IsSelected = false;
                }
                if (_wallet.Withdraw.IsSelected){
                    _wallet.Wallet.Withdraw(searchTerm);
                    _wallet.Withdraw.IsSelected = false;
                }
            }
        }
    }
    public void Update(){
        _wallet.Update();
    }
    public void Draw(){
        _wallet.Draw();
    }
    public Page_type NextState{
        get=> _nextState;
    }
    public StockItem GetItem(){
        return itemClicked;
    }
}