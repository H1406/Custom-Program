using System;
public interface ITradingStrategy{
    string GetAdvice(double avgPredictedPrice,double currentPrice);
}