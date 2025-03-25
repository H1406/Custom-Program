using static SplashKitSDK.SplashKit;
using SplashKitSDK;
using System;

public class WalletPage:Page{
    private Wallet wallet = new Wallet(100,0);
    private StockSession _stockSession = new StockSession();
    private static WalletPage _instance;
    private static object _lock = new object();
    private Transaction _inputTerm = new Transaction(200, 200, 200, 100);
    private DepositButton _depositbutton = new DepositButton("Deposit", 110, 80);
    private WithDrawButton _withdrawbutton = new WithDrawButton("Withdraw", 210, 80);
    private WalletPage(){
        Load();
    }
    public static WalletPage GetInstance(){
        if (_instance == null){
            lock (_lock){
                if (_instance == null){
                    _instance = new WalletPage();
                }
            }
        }
        return _instance;
    }
    public override void Draw(){
        NavigationBar.Draw();
        ScrollBar.Draw();
        Rectangle subWindow = new Rectangle(){
            X = 110,
            Y = 140,
            Width = 680,
            Height = 500,
        };
        SetClip(subWindow);
        DrawRectangle(ColorBlack(), subWindow);
        _stockSession.DrawWithNoButtons();
        ResetClip();
        wallet.Draw();
        _depositbutton.Draw();
        _withdrawbutton.Draw();
        if (_depositbutton.IsSelected){
            _withdrawbutton.IsSelected = false;
            _inputTerm.Draw();
        }
        if (_withdrawbutton.IsSelected){
            _depositbutton.IsSelected = false;
            _inputTerm.Draw();
        }
    }
    public void Load(){
        foreach (StockItem stock in Wallet.Stocks){
            _stockSession.AddStock(stock, 140);
        }
    }
    public override void Update(){
        if (_depositbutton.IsSelected){
            _inputTerm.Activate();
        }
        if (_withdrawbutton.IsSelected){
            _inputTerm.Activate();
        }
        _stockSession.Update(ScrollBar.GetScrollValue());
        ScrollBar.Update();
    }
    public DepositButton Deposit{
        get=>_depositbutton;
    }
    public WithDrawButton Withdraw{
        get=>_withdrawbutton;
    }
    public string InputTerm{
        get=>_inputTerm.InputTerm;
    }
    public StockSession Stocks{
        get=>_stockSession;
    }
    public Wallet Wallet{get=>wallet;}
}