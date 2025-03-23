public class PageFactory{
    public static Page CreatePage(string type){
        Page page = null;
        switch(type){
            case "home":
                page = HomePage.GetInstance();
                break;
            case "detail":
                page = DetailPage.GetInstance();
                break;
            case "follow":
                page = FollowPage.GetInstance();
                break;
            case "wallet":
                page = WalletPage.GetInstance();
                break;
        }
        return page;
    }
}