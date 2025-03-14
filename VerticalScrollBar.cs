using static SplashKitSDK.SplashKit;
using System;
public class VerticalScrollbar:Item,IScrollBar{
    private bool _isDragging;
    private float HeightLimit = 560;
    private float lastY;
    
    public VerticalScrollbar(float x, float y):base(x,y){
        Width = 10;
        Height = 40;
    }
    public void Update(){
        HandleMouseInput();
    }
    public override void Draw()
    {
        Update();
        FillRectangle(ColorWhite(), X, Y, Width, Height);
    }
    public void HandleMouseInput(){
        lastY = Y;
        if (MouseDown(SplashKitSDK.MouseButton.LeftButton)){
            if (IsClicked(MouseX(), MouseY())){
                _isDragging = true;
            }
            if (_isDragging){
                Y = MouseY() - Height/2;
                if(Y < 0){
                    Y =0;
                }
                else if (Y > HeightLimit){
                    Y = HeightLimit;
                }
            }
        }else _isDragging = false;
    }
    public float GetScrollValue(){
        return lastY - Y;
    }
}