using static SplashKitSDK.SplashKit;

public class DeleteButton : Button{
    private Item _item;
    public DeleteButton(string title, float x, float y, string filename,Item item):base(title,x,y,filename = "images/delete.png"){
        Width = 25;
        Height = 25;
        _item = item;
        BMP = LoadBitmap($"{title} delete","images/delete.png");
    }
    public override void Draw(){
        DrawBitmap(BMP,X,Y);
    }

    public Item Item{get=>_item;} 
}