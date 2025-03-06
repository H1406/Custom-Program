public class PageFactory{
    public static Page CreatePage(string type){
        Page page = null;
        switch(type){
            case "home":
                page = new HomePage("Home");
                break;
            case "detail":
                page = new DetailPage("Detail");
                break;
            case "follow":
                page = new FollowPage("Follow");
                break;
        }
        return page;
    }
}