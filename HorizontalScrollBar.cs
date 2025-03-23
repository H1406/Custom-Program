using static SplashKitSDK.SplashKit;
using System;
using SplashKitSDK;
public class HorizontalScrollBar : Item, IScrollBar{
    private bool _isDragging = false;
    private int WidthMax = 0;
    private int WidthLimit = 0;
    private float lastX;

    public HorizontalScrollBar(float x, float y,int WidthLimit,int WidthMax):base(x,y){
        Width = 40;
        Height = 10;
        this.WidthLimit = WidthLimit;
        this.WidthMax = WidthMax;
    }
    public override void Draw(){
        FillRectangle(RGBColor(128,128,128), X, Y, Width, Height);
    }
    public void Update(){
        HandleMouseInput();
    }
    public void HandleMouseInput(){
        lastX = X;
        if (MouseDown(MouseButton.LeftButton)){
            if (IsClicked(MouseX(), MouseY())){
                _isDragging = true;
            }
            if (_isDragging){
                X = MouseX() - Width/2;
                if(X > WidthMax){
                    X = WidthMax;
                }
                else if (X < WidthLimit){
                    X = WidthLimit;
                }
            }
        }else _isDragging = false;
    }
    public float GetScrollValue(){
        return (lastX-WidthLimit)/(WidthMax-WidthLimit);
    }
}