using System;
using System.Security.Cryptography.X509Certificates;
using static SplashKitSDK.SplashKit;
using SplashKitSDK;

public class DetailPage:Page{
    private StockItem? _item;
    private Graph? _graph;
    private Graph _predictGraph;
    private Button Analyze  = new Button("Analyze", 600, 100, "Analyze");
    private Analyzer _analyzer = new Analyzer();
    public DetailPage(){}

    public override void Draw(){
        SetUp();
        ScrollBar.Draw();
        NavigationBar.Draw();
        _item.Draw();
        Analyze.Draw();
        if (_graph != null){
            Rectangle subWindow = new Rectangle(){
                X = 120,
                Y = 100,
                Width = 400,
                Height = 200,
            };
            SetClip(subWindow);
            DrawRectangle(ColorBlack(), subWindow);
            _graph.Draw();
            ResetClip();
        }
        if(_predictGraph != null){
            Rectangle subWindow1 = new Rectangle(){
                X = 120,
                Y = 350,
                Width = 400,
                Height = 200,
            };
            SetClip(subWindow1);
            DrawRectangle(ColorBlack(), subWindow1);
            _predictGraph.Draw();
            ResetClip();
        }
    }
    public StockItem Item{
        get => _item;
        set{
            _item = value;
        }
    }
    public void SetUp(){
        _item.X = 150;
        _item.Y = 20;
    }
    public Graph PredictGraph{get=> _predictGraph; set=> _predictGraph = value;}
    public Graph Graph{get=> _graph; set=> _graph = value;}
    public Button AnalyzeButton{get=> Analyze;}
    public Analyzer Analyzer{get=> _analyzer;}
}