using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System;
using System.IO;
public class Wallet: Item
{
    private static string FontName = "Arial";
    private static int TextSize = 50;
    private DatabaseManager _manager = new DatabaseManager();
    private List<StockItem> _stocks = new List<StockItem>();
    private double total = 0;
    private double cash = 0;
    private const string filepath = "wallet.txt";
    public Wallet(float x,float y):base(x,y){
        Load();
    }
    public override void Draw()
    {
        DrawText("Portfolio",ColorWhite(),FontName,TextSize, X+300, Y+20);
        DrawText($"Total: {total}$",ColorWhite(),FontName,TextSize, X+20, Y+50);
        DrawText($"Cash: {cash}$",ColorWhite(),FontName,TextSize, X+200, Y+50);
    }
    public void Load(){
        _manager.GetWallet().Wait();
        StreamReader reader = new StreamReader(filepath);
        cash = Convert.ToDouble(reader.ReadLine());
        _stocks = _manager.Stocks;
        total += cash;
        foreach(StockItem stock in _stocks){
            total += stock.Current * stock.Quantity;
        }
    }
    public void Deposit(string amount){
        cash += Convert.ToInt32(amount);
        total += Convert.ToInt32(amount);
        SaveMoney();
    }
    public void Withdraw(string amount){
        if (cash < Convert.ToDouble(amount)){
            Console.WriteLine("Insufficient funds");
            FadeMusicIn(new Music("invalid","invalid.mp3"),1000);
            return;
        }
        cash -= Convert.ToDouble(amount);
        total -= Convert.ToDouble(amount);
        SaveMoney();
    }
    public void Buy(StockItem stock,string amount){
        if (cash < stock.Current * Convert.ToInt32(amount)){
            Console.WriteLine("Insufficient funds");
            Console.WriteLine($"You need {stock.Current * Convert.ToInt32(amount)}");
            FadeMusicIn(new Music("invalid","invalid.mp3"),1000);
            return;
        }
        cash -= stock.Current * Convert.ToInt32(amount);
        total -= stock.Current * Convert.ToInt32(amount);
        stock.Quantity += Convert.ToInt32(amount);
        SaveQuant(stock);
        SaveMoney();
    }
    public void Sell(StockItem stock,string amount){
        if (stock.Quantity < Convert.ToInt32(amount)){
            Console.WriteLine("Insufficient stock");
            FadeMusicIn(new Music("invalid","invalid.mp3"),1000);
            return;
        }
        cash += stock.Current * Convert.ToInt32(amount);
        total += stock.Current * Convert.ToInt32(amount);
        stock.Quantity -= Convert.ToInt32(amount);  
        SaveQuant(stock);
        SaveMoney();
    }
    public void SaveQuant(StockItem stock){
        _manager.SaveWallet(stock);
    }
    public void SaveMoney(){
        StreamWriter writer = new StreamWriter(filepath);
        writer.WriteLine(cash);
        writer.Close();
    }
    public double Total{get=>total;}
    public List<StockItem> Stocks{get=>_stocks;}
}