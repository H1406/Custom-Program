public class PageFactory{
    public static Page CreatePage(string type){
        Page page = null;
        switch(type){
            case "home":
                page = new HomePage();
                break;
            case "detail":
                page = new DetailPage();
                break;
            case "follow":
                page = new FollowPage();
                break;
        }
        return page;
    }
}