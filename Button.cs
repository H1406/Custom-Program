using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Drawing;
public class Button:Item
{
    private Bitmap _bitmap;
    private const string  FontName = "Arial"; 
    public Button(string name,float x, float y,string filename):base(name,x,y){
        _bitmap = LoadBitmap(name, filename);
        Width = 100;
        Height = 100;   
    }
    public Button(string text, float x, float y):base(text,x,y){
        Width = 100;
        Height = 30;
    }
    public override bool IsClicked(float mousex, float mousey)
    {
        if (mousex >= X && mousex <= Width + X && mousey >= Y && mousey <= Y + Height ){
            return true;
        }
        return false;
    }
    public override void Draw(){
        if (_bitmap == null){
            DrawRectangle(ColorWhite(), X, Y, Width, Height);
            DrawText(Name,ColorWhite(),FontName,50, X+20, Y+Height/2);
            return;
        }
        DrawBitmap(_bitmap, X, Y);
    }
    public Bitmap BMP{get=>_bitmap;set=>_bitmap=value;}
}