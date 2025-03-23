using System;
using static SplashKitSDK.SplashKit;

public class NavigationBar:Item
{
    private Button homeButton = new Button("home",0,40,"images/home.png");
    private Button followingButton = new Button("follow",0,180,"images/follow.png");
    private Button walletButton = new Button("wallet",0,360,"images/wallet.png");
    private List<Button> buttons = new List<Button>();

    public NavigationBar(float x, float y):base(x,y){
        buttons.Add(homeButton);
        buttons.Add(walletButton);
        buttons.Add(followingButton);
    }
    public string PageClicked(){
        foreach (Button button in buttons){
            if(button.IsClicked(MouseX(),MouseY())){
                return button.Name;
            }
        }
        return "";
    }

    public override void Draw(){
        DrawLine(ColorBlack(), X, Y, X, Y+10000);
        homeButton.Draw();
        followingButton.Draw();
        walletButton.Draw();
    }
}