using static SplashKitSDK.SplashKit;
using SplashKitSDK;
using System.Drawing;
using System;
using System.Security.Cryptography.X509Certificates;
public class Transaction:Item{
    private TextInput _input ;
    private Button _confirmButton;
    private Button _cancelButton;
    public Transaction(float x, float y, int width, int height):base(x,y,width,height){
        _input = new TextInput(x+10,y+20,width-20,30);
        _confirmButton = new Button("confirm",x+20,y+60);
        _cancelButton = new Button("cancel",x+width-70,y+60);
    }
    public  override void Draw(){
        FillRectangle(ColorBlack(), X, Y, Width, Height);
        _confirmButton.Draw();
        _cancelButton.Draw();
        _input.Draw();
    }
    public void Activate(){
        _input.Activate();
    }
    public void Deactivate(){
        _input.Deactivate();
    }
    public string InputTerm{get=> _input.InputTerm;}
    public Button ConfirmButton{get=> _confirmButton;}
    public Button CancelButton{get=> _cancelButton;}
}