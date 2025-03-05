namespace StockApp;
public class AppContext
{
    private IAppState? _currentState;

    public void SetState(IAppState state)
    {
        _currentState = state;
    }
    public void Update(){
        _currentState.Update();
    }

    public void HandleInput()
    {
        _currentState.HandleInput();
    }

    public void Draw()
    {
        _currentState.Draw();
    }

    public Page_type GetNextState()
    {
        return _currentState.NextState;
    }
    public StockItem GetItem(){
        return _currentState.GetItem();
    }
}