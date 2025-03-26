using System;
using Microsoft.Data.Sqlite;

public class DatabaseManager
{
    private List<StockItem> _stocks = new List<StockItem>();
    private StockFetcher _fetcher = new StockFetcher();
    private const string DatabaseFile = "Data/StockData.db";
    public DatabaseManager()
    {
        using(var connection = new SqliteConnection($"Data Source={DatabaseFile}")){
            connection.Open();
            string tableCommand = @"CREATE TABLE IF NOT EXISTS StockData (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                symbol TEXT NOT NULL,
                amount INTEGER NOT NULL
                )";
            using(var command = new SqliteCommand(tableCommand, connection)){
                command.ExecuteNonQuery();
            }
        }
    }
    public void SaveStock(StockItem stock){
    try{
        using (var connection = new SqliteConnection($"Data Source={DatabaseFile}"))
        {
            connection.Open();
            string insertCommand = @"INSERT INTO StockData (symbol) VALUES (@symbol)";
            using (var command = new SqliteCommand(insertCommand, connection))
            {
                command.Parameters.AddWithValue("@symbol", stock.Name);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine($"Stock {stock.Name} saved successfully!");
    }
    catch (Exception ex){
        Console.WriteLine($"Error saving stock: {ex.Message}");
        }
    }       

    public void RemoveStock(StockItem stock){
        using(var connection = new SqliteConnection($"Data Source={DatabaseFile}")){
            connection.Open();
            string deleteCommand = @"DELETE FROM StockData WHERE symbol = @symbol";
            using(var command = new SqliteCommand(deleteCommand, connection)){
                command.Parameters.AddWithValue("@symbol", stock.Name);
                command.ExecuteNonQuery();
            }
        }
    }
    public async Task LoadData(){
        using(var connection = new SqliteConnection($"Data Source={DatabaseFile}")){
            connection.Open();
            string selectCommand = @"SELECT symbol,amount FROM StockData";
            using(var command = new SqliteCommand(selectCommand, connection)){
                using(var reader = command.ExecuteReader()){
                    while(reader.Read()){
                        string symbol = reader.GetString(0);
                        int amount = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        await _fetcher.FetchStockData(symbol);
                        _fetcher.ItemFound.Quantity = amount;
                        if (_fetcher.ItemFound.Quantity > 0){
                            Console.WriteLine($"Stock {symbol} loaded successfully!");
                        }
                        if(_fetcher.ItemFound == null){
                            Console.WriteLine($"No data found for {symbol}");
                        }
                        _stocks.Add(_fetcher.ItemFound);
                    }
                }
            }
        }
    }
    public async Task GetWallet(){
        using(var connection = new SqliteConnection($"Data Source={DatabaseFile}")){
            connection.Open();
            string selectCommand = @"SELECT symbol FROM StockData WHERE amount > 0";
            using(var command = new SqliteCommand(selectCommand, connection)){
                using(var reader = command.ExecuteReader()){
                    while(reader.Read()){
                        string symbol = reader.GetString(0);
                        await _fetcher.FetchStockData(symbol);
                        if(_fetcher.ItemFound == null){
                            Console.WriteLine($"No data found for {symbol}");
                        }
                        _stocks.Add(_fetcher.ItemFound);
                    }
                }
            }
        }
    }
    public void SaveWallet(StockItem stock){
        using(var connection = new SqliteConnection($"Data Source={DatabaseFile}")){
            connection.Open();
            string updateCommand = @"Update StockData SET amount = @amount WHERE symbol = @symbol";
            using(var command = new SqliteCommand(updateCommand, connection)){
                command.Parameters.AddWithValue("@amount", stock.Quantity);
                command.Parameters.AddWithValue("@symbol", stock.Name);
                command.ExecuteNonQuery();
            }
        }
    }
    public StockItem LoadStock(string symbol){
        _fetcher.FetchStockData(symbol).Wait();
        return _fetcher.ItemFound;
    }
    public List<StockItem> Stocks{get=>_stocks;}
}