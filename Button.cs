using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Drawing;
public class Button:Item
{
    private Bitmap? _bitmap = null;
    private const string  FontName = "Arial"; 
    public Button(string name,float x, float y,string filename):base(name,x,y){
        _bitmap = LoadBitmap(name, filename);
        Width = 100;
        Height = 100;   
    }
    public Button(string text, float x, float y):base(text,x,y){
        Width = TextWidth(text, FontName, 20)+10;  
        Height = TextHeight(text, FontName, 20)+5;
    }
    public override bool IsClicked(float mousex, float mousey)
    {
        if (mousex >= X && mousex <= Width + X && mousey >= Y && mousey <= Y + Height ){
            return true;
        }
        return false;
    }
    public override void Draw(){
        if(IsHovered()){
            FillRectangle(RGBColor(128,128,128), X, Y, Width, Height);
        }
        if (_bitmap == null){
            DrawRectangle(ColorWhite(), X, Y, Width, Height);
            DrawText(Name,ColorWhite(),FontName,20, X+5, Y);
        }
        else DrawBitmap(_bitmap, X, Y);
    }
    public Bitmap BMP{get=>_bitmap;set=>_bitmap=value;}
}