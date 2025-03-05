namespace StockApp;
public interface IAppState
{
    void HandleInput();
    void Draw();
    void Update();
    Page_type NextState{get;}
    StockItem GetItem();

}