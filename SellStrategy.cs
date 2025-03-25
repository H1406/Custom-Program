public class SellStrategy:ITradingStrategy{
    public string GetAdvice(double avgPredictedPrice,double currentPrice){
        if(avgPredictedPrice < currentPrice*0.9){
            return $"Sell: Expected price is {avgPredictedPrice:F2} and current price is {currentPrice}.Should sell 2/3";
        }
        return $"Sell: Expected price is {avgPredictedPrice:F2} and current price is {currentPrice}.Should sell 1/3";
    }
}