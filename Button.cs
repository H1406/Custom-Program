using SplashKitSDK;
using static SplashKitSDK.SplashKit;
public class Button:Item
{
    private Bitmap _bitmap;
    public Button(string name,float x, float y,string filename):base(name,x,y){
        _bitmap = LoadBitmap(name, filename);
        Width = 100;
        Height = 100;   
    }
    public override bool IsClicked(float mousex, float mousey)
    {
        if (mousex >= X && mousex <= Width + X && mousey >= Y && mousey <= Y + Height ){
            return true;
        }
        return false;
    }
    public override void Draw(){
        // DrawingOptions opts = OptionScaleBmp(0.1, 0.1);
        DrawBitmap(_bitmap, X, Y);
    }
    public Bitmap BMP{get=>_bitmap;set=>_bitmap=value;}
}