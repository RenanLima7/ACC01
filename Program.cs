using ACC01;
using ACC01.Algorithms;
using ACC01.Algorithms.Interfaces;
using ACC01.Logger;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = Serilloger.GetInstance();

        var testCases = new List<Knapsack>
        {
            new Knapsack(weights: new int[] { 1, 2, 3 }, values: new int[] { 10, 20, 30 }, capacity: 5),
            new Knapsack(weights: new int[] { 2, 3, 4, 5 }, values: new int[] { 3, 4, 5, 6 }, capacity: 5),
            new Knapsack(weights: new int[] { 1, 4, 5, 7 }, values: new int[] { 1, 3, 4, 5 }, capacity: 7),
            new Knapsack(weights: new int[] { 1, 3, 4, 5, 6 }, values: new int[] { 2, 3, 7, 8, 9 }, capacity: 10),
            new Knapsack(weights: new int[] { 3, 2, 4, 1, 5 }, values: new int[] { 30, 20, 50, 10, 40 }, capacity: 8)
        };

        var algorithms = new Dictionary<string, IAlgorithm>
        {
            { "BruteForce", AlgorithmFactory.GetAlgorithm(AlgorithmType.BruteForce) },
            { "DivideAndConquer", AlgorithmFactory.GetAlgorithm(AlgorithmType.DivideAndConquer) },
            { "DynamicProgramming", AlgorithmFactory.GetAlgorithm(AlgorithmType.DynamicProgramming) }
        };

        foreach (var testCase in testCases)
        {
            Console.WriteLine("\nTESTING KNAPSACK:\n");            
            Console.WriteLine($"Weights: {string.Join(", ", testCase.Weights)}");
            Console.WriteLine($"Values: {string.Join(", ", testCase.Values)}");
            Console.WriteLine($"Capacity: {testCase.Capacity}");
            Console.WriteLine("---------------------------------\n");

            foreach (var algo in algorithms)
            {
                RunAndLogAlgorithm(algo.Key, algo.Value, testCase, logger);

                Thread.Sleep(300);
            }

            Console.WriteLine();
        }
    }

    public static void RunAndLogAlgorithm(string algorithmName, IAlgorithm algorithm, Knapsack knapsack, Serilloger logger)
    {
        GC.Collect();
        long memoryBefore = GC.GetTotalMemory(true);

        Stopwatch stopwatch = Stopwatch.StartNew();
        int result = algorithm.Solve(knapsack);
        stopwatch.Stop();

        long memoryAfter = GC.GetTotalMemory(true);
        long memoryUsed = memoryAfter - memoryBefore;

        logger.Log(
            algorithm: algorithmName,
            type: "Knapsack Problem",
            timeInMs: stopwatch.ElapsedMilliseconds,
            memoryUsed: memoryUsed
        );

        Console.WriteLine($"Algorithm: {algorithmName}");
        Console.WriteLine($"Result: {result}");
        Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Memory Used: {memoryUsed / 1024} KB");
        Console.WriteLine("---------------------------------");
    }
}
