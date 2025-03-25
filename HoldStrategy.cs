public class HoldStrategy:ITradingStrategy{
    public string GetAdvice(double avgPredictedPrice,double currentPrice){
        return $"Hold: Expected price is {avgPredictedPrice} and current price is {currentPrice}. There is no significant change in price.";
    }
}