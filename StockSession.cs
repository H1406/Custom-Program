using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
public class StockSession
{
    private ButtonSession buttons = new ButtonSession();
    private List<StockItem> _stocks = new List<StockItem>();
    public StockSession(){
    }
    public StockItem StockClicked(float x, float y){
        foreach(StockItem stock in _stocks){
            if(stock.IsClicked(x,y)){
                return stock;
            }
        }
        return null;
    }
   public void AddStock(StockItem stock)
    {
        if (stock == null)
        {
            throw new ArgumentNullException(nameof(stock), "StockItem cannot be null.");
        }

        foreach (StockItem _stock in _stocks)
        {
            if (_stock.Name == stock.Name)
            {
                return;
            }
        }

        _stocks.Add(stock);
        buttons.Clear();
        foreach (StockItem _stock in _stocks)
        {
            _stock.X = 110;
            _stock.Y = _stocks.IndexOf(_stock) * 20 + 100;
            buttons.AddButton(_stock);
        }
    }
    public void RemoveStock(StockItem stock){
        _stocks.Remove(stock);
    }
    public void LoadStocks(List<StockItem> stocks){
        if (stocks == null)
        {
            throw new ArgumentNullException(nameof(stocks), "Stocks list cannot be null.");
        }

        _stocks.Clear();
        for (int i = 0; i < stocks.Count; i++)
        {
            if (stocks[i] == null)
            {
                throw new ArgumentNullException(nameof(stocks), "StockItem in the list cannot be null.");
            }
            AddStock(stocks[i]);
        }
    }
    public void Draw(){
        foreach(StockItem stock in _stocks){
            stock.Draw();
        }
        buttons.Draw();
    }
    public void Update(float delta){
        foreach(StockItem stock in _stocks){
            stock.Y = stock.Y + delta;
        }
        buttons.Update(delta);
    }
    public int StockCount{
        get => _stocks.Count;
    }
    public List<StockItem> Stocks => _stocks;
    public ButtonSession Buttons => buttons;
    
}