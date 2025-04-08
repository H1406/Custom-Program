using static SplashKitSDK.SplashKit;
using System;

public class ButtonSession{
    private List<Button> _addbuttons = new List<Button>();
    private List<Button> _delbuttons = new List<Button>();
    public ButtonSession(){
    }
    public void AddButton(StockItem stock){
        //Generate add and delete buttons
        AddButton add_button = new AddButton(stock.Name,720,stock.Y+17,"images/add.png",stock);
        DeleteButton del_button = new DeleteButton(stock.Name,755,stock.Y+17,"images/delete.png",stock);
        _addbuttons.Add(add_button);
        _delbuttons.Add(del_button);
    }
    public void RemoveButton(StockItem stock){
        foreach(AddButton button in _addbuttons){
            if(button.Item == stock){
                _addbuttons.Remove(button);
                break;
            }
        }
    }
    public StockItem StockAdd(float x, float y){
        foreach (AddButton button in _addbuttons)
        {
            if(button.IsClicked(x,y)){
                return button.Item as StockItem;
            }
        }
        return null;
    }
    public StockItem StockDelete(float x, float y){
        foreach (DeleteButton button in _delbuttons)
        {
            if(button.IsClicked(x,y)){
                RemoveButton(button.Item as StockItem);
                _delbuttons.Remove(button);
                return button.Item as StockItem;
            }
        }
        return null;
    }
    public void Draw(){ 
        foreach(Button button in _addbuttons){
            button.Draw();
        }
        foreach(Button button in _delbuttons){
            button.Draw();
        }
    }
    public void Update(float delta){
        for (int i = 0; i < _addbuttons.Count; i++){
            _addbuttons[i].Y = _addbuttons[i].Y + delta;
            _delbuttons[i].Y = _delbuttons[i].Y + delta;
        }
    }  
    public void Clear(){
        _addbuttons.Clear();
        _delbuttons.Clear();
    }
}