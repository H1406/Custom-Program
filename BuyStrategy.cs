using System;

public class BuyStrategy : ITradingStrategy{
    public BuyStrategy(){
    }
    public string GetAdvice(double avgPredictedPrice,double currentPrice){
        if(avgPredictedPrice > currentPrice*1.2){
            return $"Buy: Expected price is {avgPredictedPrice:F2} and current price is {currentPrice}.Should all in";
        }
        return $"Buy: Expected price is {avgPredictedPrice:F2} and current price is {currentPrice}";
    }
}