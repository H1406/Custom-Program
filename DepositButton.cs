using static SplashKitSDK.SplashKit;
using SplashKitSDK;
using System.Drawing;
public class DepositButton : Button
{
    private bool _isSelected = false;
    public DepositButton(string text,float x,float y) : base("Deposit",x,y)
    {
    }
    public override void Draw(){
        if(_isSelected){
            FillRectangle(RGBColor(128,128,128), X, Y, Width, Height);
        }
        base.Draw();
    }
    public override bool IsClicked(float mousex, float mousey){
        if (mousex >= X && mousex <= Width + X && mousey >= Y && mousey <= Y + Height){
            if(!_isSelected){
                _isSelected = true;
            }else{
                _isSelected = false;
            }
            return true;
        }
        return false;
    }
    public void Update(){
    }
    public bool IsSelected{get=>_isSelected;set=>_isSelected=value;}

}
