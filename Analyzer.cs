
public class Analyzer{
    private double[] predictedPrices = new double[10];
    private TradingContext _tradingContext = new TradingContext();
    public Analyzer(){
    }
    public void Analyze(string symbol){
        string filePath = $"Historical Prices/{symbol}.csv"; // Your CSV file
        double[] stockPrices = CSVReader.LoadStockPrices(filePath);
        if (stockPrices.Length == 0)
        {
            Console.WriteLine("No data found in the CSV file.");
            return;
        }

        // Normalize Data
        (double[] normalizedPrices, double min, double max) = DataPreprocessing.Normalize(stockPrices);

        // Convert to Sequences
        int sequenceLength = 5; // Use last 5 days to predict next day
        (double[][] sequences, double[] labels) = SequenceGenerator.CreateSequences(normalizedPrices, sequenceLength);

        // Create LSTM Model
        int inputSize = sequenceLength;
        int hiddenSize = 10;
        LSTMCell lstm = new LSTMCell(inputSize, hiddenSize);

        // Training parameters
        int epochs = 100;
        double learningRate = 0.01;

        // Step 2: Train the model
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            double totalLoss = 0;

            for (int i = 0; i < sequences.Length; i++)
            {
                // Forward pass
                double[] output = lstm.Forward(sequences[i]);

                // Compute loss (e.g., Mean Squared Error)
                double loss = ComputeMeanSquaredError(output[0], labels[i]);
                totalLoss += loss;

                // Compute gradients
                double[] dLoss_dh = new double[hiddenSize];
                for (int j = 0; j < hiddenSize; j++)
                {
                    dLoss_dh[j] = 2 * (output[j] - labels[i]); // Derivative of MSE
                }
                double[] dLoss_dc_next = new double[hiddenSize]; // Assume zero for the last time step
                var (dWf, dWi, dWc, dWo, dbf, dbi, dbc, dbo) = lstm.Backward(dLoss_dh, dLoss_dc_next);

                // Update weights and biases using gradient descent
                lstm.UpdateWeights(dWf, dWi, dWc, dWo, dbf, dbi, dbc, dbo, learningRate);
            }

            // Console.WriteLine($"Epoch {epoch + 1}, Loss: {totalLoss / sequences.Length}");
        }
        double[] lastSequence = normalizedPrices.Skip(normalizedPrices.Length - sequenceLength).ToArray();

        // Step 3: Predict prices for the next 10 days
        for (int i = 0; i < 10; i++)
        {
            // Predict the next day's price
            double[] output = lstm.Forward(lastSequence);
            double predictedPrice = DataPreprocessing.Denormalize(output[0], min, max);
            predictedPrices[i] = predictedPrice;

            // Update the input sequence for the next prediction
            lastSequence = lastSequence.Skip(1).Concat(new double[] { output[0] }).ToArray();
        }

        // Step 4: Print the predicted prices
        Console.WriteLine("Predicted Prices for the Next 10 Days:");
        for (int i = 0; i < predictedPrices.Length; i++)
        {
            Console.WriteLine($"Day {i + 1}: {predictedPrices[i]:F2}");
        }
    }
    
    static double ComputeMeanSquaredError(double predicted, double actual)
    {
        return Math.Pow(predicted - actual, 2);
    }  
    public string ExecuteStrategy(double currentPrice){
        _tradingContext = new TradingContext();
        double avgFuturePrice = predictedPrices.Average(); 
        //Execute trading strategy based on average future price
        if (avgFuturePrice > currentPrice * 1.05)
        {
            _tradingContext.SetStrategy(new BuyStrategy());
        }
        else if (avgFuturePrice < currentPrice * 0.95)
        {
            _tradingContext.SetStrategy(new SellStrategy());
        }
        else
        {
            _tradingContext.SetStrategy(new HoldStrategy());
        }
        return _tradingContext.ExecuteStrategy(avgFuturePrice, currentPrice);
    } 
    public double[] PredictedPrices{get => predictedPrices;}
}
