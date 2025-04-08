using static SplashKitSDK.SplashKit;

public class AddButton : Button{
    private Item _item;
    public AddButton(string title, float x, float y, string filename,Item item):base(title,x,y,filename){
        //Setup button with title, x, y, filename
        Width = 25;
        Height = 25;
        _item = item;
        BMP = LoadBitmap($"{title} add","images/add.png");
    }
    public override void Draw(){
        DrawBitmap(BMP, X, Y);
    }
    public Item Item{get=>_item;} 
}