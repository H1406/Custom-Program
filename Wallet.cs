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
        DrawText($"Total: {total}",ColorWhite(),FontName,TextSize, X+20, Y+50);
        DrawText($"Cash: {cash}",ColorWhite(),FontName,TextSize, X+200, Y+50);
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
    }
    public void Withdraw(string amount){
        if (cash < Convert.ToDouble(amount)){
            Console.WriteLine("Insufficient funds");
            return;
        }
        cash -= Convert.ToDouble(amount);
        total -= Convert.ToDouble(amount);
    }
    public void Buy(StockItem stock,string amount){
        if (cash < stock.Current * Convert.ToInt32(amount)){
            Console.WriteLine("Insufficient funds");
            Console.WriteLine($"You need {stock.Current * Convert.ToInt32(amount)}");
            return;
        }
        cash -= stock.Current * Convert.ToInt32(amount);
        total -= stock.Current * Convert.ToInt32(amount);
        stock.Quantity += Convert.ToInt32(amount);
        Save(stock);
    }
    public void Sell(StockItem stock,string amount){
        if (stock.Quantity < Convert.ToInt32(amount)){
            Console.WriteLine("Insufficient stock");
            return;
        }
        cash += stock.Current * Convert.ToInt32(amount);
        total += stock.Current * Convert.ToInt32(amount);
        stock.Quantity -= Convert.ToInt32(amount);  
        Save(stock);
    }
    public void Save(StockItem stock){
        StreamWriter writer = new StreamWriter(filepath);
        writer.WriteLine(cash);
        writer.Close();
        _manager.SaveWallet(stock);
    }
    public double Total{get=>total;}
    public List<StockItem> Stocks{get=>_stocks;}
}