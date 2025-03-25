public class TradingContext{
    private ITradingStrategy _strategy;
    public TradingContext(){
    }
    public void SetStrategy(ITradingStrategy strategy){
        _strategy = strategy;
    }
    public string ExecuteStrategy(double avgPredictedPrice, double currentPrice){
        return _strategy.GetAdvice( avgPredictedPrice, currentPrice);
    }
}