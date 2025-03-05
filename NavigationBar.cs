using System;
using static SplashKitSDK.SplashKit;

public class NavigationBar:Item
{
    private Button homeButton = new Button("home",15,40,"images/home.png");
    private Button followingButton = new Button("follow",15,180,"images/follow.png");
    private Button analysisButton = new Button("analysis",15,360,"images/analysis.png");
    private List<Button> buttons = new List<Button>();

    public NavigationBar(string title,float x, float y):base(title,x,y){
        buttons.Add(homeButton);
        buttons.Add(analysisButton);
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
        analysisButton.Draw();
    }
}