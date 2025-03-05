using System;
using System.Drawing;
using static SplashKitSDK.SplashKit;
using SplashKitSDK;

namespace StockApp
{
    public class Program
    
    {
        private static SplashKitSDK.Color Background = RGBColor(64, 64, 64);
        public static void Main(){
            HomePage home = new HomePage("home");
            FollowPage follow = new FollowPage("follow");
            DetailPage detail = new DetailPage("detail");

            AppContext context = new AppContext();
            context.SetState(new HomeState(home, follow,detail));

            OpenWindow("Stock App", 800, 600);
            do{
                ClearScreen(Background);
                ProcessEvents();

                // Handle input and draw the current state
                context.HandleInput();
                context.Update();
                context.Draw();
                // Check if a state transition is needed
                Page_type nextState = context.GetNextState();
                StockItem item = context.GetItem();
                if (nextState != Page_type.home) // Transition to a new state
                {
                    switch (nextState)
                    {
                        case Page_type.following:
                            context.SetState(new FollowState(follow, home, detail));
                            break;
                        case Page_type.detail:
                            context.SetState(new DetailState(detail,new StockItem(item.Name, item.X, item.Y, item.High, item.Low, item.Open, item.Current),home));
                            break;
                    }
                }
                else{
                    context.SetState(new HomeState(home, follow,detail));
                }
                RefreshScreen();
            } while (!QuitRequested());
        }
    }
}
