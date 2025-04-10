using System;
using static SplashKitSDK.SplashKit;
using SplashKitSDK;

public abstract class Item{
    private string _name;
    private float _x;
    private float _y;
    private int _width;
    private int _height;
    public Item(string name,float x, float y){
        _x = x;
        _y = y;
        _name = name;
    }
    public Item(float x,float y){
        _name = "default";
        _x = x;
        _y = y;
    }
    public Item(float x, float y, int width, int height){
        _x = x;
        _y = y;
        _width = width;
        _height = height;
    }
    public abstract void Draw();
    public float X{
        get{
            return _x;
        }
        set{
            _x = value;
        }
    }
    public float Y{
        get{
            return _y;
        }
        set{
            _y = value;
        }
    }
    public string Name{
        get{
            return _name;
        }
        set{
            _name = value;
        }
    }
    //Check if the item is hovered
    public virtual bool IsHovered(){
        float mousex = MouseX();
        float mousey = MouseY();
        if( mousex >= X && mousex <= Width + X && mousey >= Y && mousey <= Y + Height){
            return true;
        }
        return false;
    }
    public int Height{get=>_height;set=>_height = value;}
    public int Width{get=>_width;set=>_width = value;}
    //Check if the item is clicked
    public virtual bool IsClicked(float mousex,float mousey ){
        if( mousex >= X && mousex <= Width + X && mousey >= Y && mousey <= Y + Height){
            return true;
        }
        return false;
    }
}