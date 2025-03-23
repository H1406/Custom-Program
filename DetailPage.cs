using System;
using static SplashKitSDK.SplashKit;
using SplashKitSDK;

public class DetailPage:Page{
    private bool _isBuy = false;
    private bool _isSell = false;
    private StockItem? _item;
    private Graph? _graph;
    private Graph _predictGraph;
    private Button _analyzebutton  = new Button("Analyze", 600, 100);
    private Button _buybutton = new Button("Buy", 570, 200);
    private Button _sellbutton = new Button("Sell",650, 200);
    private Transaction transaction = new Transaction(200,200,200,100);
    private Analyzer _analyzer = new Analyzer();
    private static DetailPage _instance;
    private static readonly object _lock = new object();
    public DetailPage(){}
    public static DetailPage GetInstance(){
        if (_instance == null){
            lock (_lock){
                if (_instance == null){
                    _instance = new DetailPage();
                }
            }
        }
        return _instance;
    }

    public override void Draw(){
        SetUp();
        NavigationBar.Draw();
        _item.Draw();
        _analyzebutton.Draw();
        _buybutton.Draw();
        _sellbutton.Draw();
        if (_graph != null){
            Rectangle subWindow = new Rectangle(){
                X = 120,
                Y = 95,
                Width = 400,
                Height = 200,
            };
            SetClip(subWindow);
            DrawRectangle(ColorBlack(), subWindow);
            _graph.Draw();
            ResetClip();
            _graph.ScrollBar.Draw();
        }
        if(_predictGraph != null){
            Rectangle subWindow1 = new Rectangle(){
                X = 120,
                Y = 345,
                Width = 400,
                Height = 200,
            };
            SetClip(subWindow1);
            DrawRectangle(ColorBlack(), subWindow1);
            _predictGraph.Draw();
            ResetClip();
            _predictGraph.ScrollBar.Draw();
        }
        if(_isBuy||_isSell){
            transaction.Draw();
        }
    }
    public override void Update(){
        if(_graph != null) _graph.Update();
        if(_predictGraph != null) _predictGraph.Update();
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
    public Button AnalyzeButton{get=> _analyzebutton;}
    public Analyzer Analyzer{get=> _analyzer;}
    public Button BuyButton{get=> _buybutton;}
    public Button SellButton{get=> _sellbutton;}
    public Transaction Transaction{get=> transaction;}
    public bool IsBuy{get=> _isBuy; set=> _isBuy = value;}
    public bool IsSell{get=> _isSell; set=> _isSell = value;}
}