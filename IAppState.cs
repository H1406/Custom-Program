namespace StockApp;
public interface IAppState
{
    void HandleInput();
    void Update();
    void Draw();
    Page_type NextState{get;}
    StockItem GetItem();

}