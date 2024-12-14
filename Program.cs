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

        var testCasesWithDescriptions = new List<(Knapsack, string)>
        {
            (new Knapsack(new int[] { 1, 2 }, new int[] { 10, 15 }, 2), "Cenário 1: Conjunto Mínimo"),
            (new Knapsack(new int[] { 1, 2, 3 }, new int[] { 10, 15, 40 }, 5), "Cenário 2: Conjunto Pequeno com Capacidade Justa"),
            (new Knapsack(new int[] { 2, 3, 4, 5 }, new int[] { 3, 4, 8, 8 }, 5), "Cenário 3: Conjunto Médio com Capacidade Limitada"),
            (new Knapsack(new int[] { 2, 3, 4, 5 }, new int[] { 3, 4, 8, 8 }, 10), "Cenário 4: Conjunto Médio com Capacidade Mais Alta"),
            (new Knapsack(new int[] { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new int[] { 1, 4, 5, 7, 8, 9, 10, 13, 14, 15 }, 20), "Cenário 5: Conjunto Grande"),
            (new Knapsack(Enumerable.Range(1, 10).ToArray(), Enumerable.Range(1, 10).ToArray(), 15), "Cenário 6: Progressivo com Pesos e Valores Iguais"),
            (new Knapsack(Enumerable.Range(1, 20).ToArray(), Enumerable.Range(1, 20).Select(x => x * 2).ToArray(), 50), "Cenário 7: Grande Conjunto"),
            (new Knapsack(new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 }, new int[] { 1, 6, 8, 10, 15, 20, 22, 30, 35, 40 }, 60), "Cenário 8: Progressivo com Pesos Aleatórios"),
            (new Knapsack(new int[] { 10, 20, 30, 40, 50 }, new int[] { 100, 200, 300, 400, 500 }, 100), "Cenário 9: Itens Grandes"),
            (new Knapsack(Enumerable.Range(1, 50).ToArray(), Enumerable.Range(1, 50).Select(x => x * 3).ToArray(), 100), "Cenário 10: Extremo")
        };

        var algorithms = new Dictionary<string, IAlgorithm>
        {
            { "BruteForce", AlgorithmFactory.GetAlgorithm(AlgorithmType.BruteForce) },
            { "DivideAndConquer", AlgorithmFactory.GetAlgorithm(AlgorithmType.DivideAndConquer) },
            { "DynamicProgramming", AlgorithmFactory.GetAlgorithm(AlgorithmType.DynamicProgramming) }
        };

        foreach (var (testCase, description) in testCasesWithDescriptions)
        {
            Console.WriteLine("\nTESTING KNAPSACK:\n");
            Console.WriteLine($"Weights: {string.Join(", ", testCase.Weights)}");
            Console.WriteLine($"Values: {string.Join(", ", testCase.Values)}");
            Console.WriteLine($"Capacity: {testCase.Capacity}");
            Console.WriteLine("---------------------------------\n");

            foreach (var algorithm in algorithms)
            {
                Console.WriteLine($"Executando {description} com {algorithm.GetType().Name}");
                RunAndLogAlgorithm(algorithm.Key, description, algorithm.Value, testCase, logger);

                Thread.Sleep(300);
            }

            Console.WriteLine();
        }
    }

    static void RunAndLogAlgorithm(string algorithmName, string type, IAlgorithm algorithm, Knapsack knapsack, Serilloger logger)
    {
        int repetitions = 1000;
        GC.Collect();
        long memoryBefore = GC.GetTotalMemory(true);

        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < repetitions; i++)
        {
            algorithm.Solve(knapsack);
        }
        stopwatch.Stop();

        long memoryAfter = GC.GetTotalMemory(true);
        long memoryUsed = memoryAfter - memoryBefore;

        long averageTimeInMs = stopwatch.ElapsedMilliseconds / repetitions;

        logger.Log(
            algorithm: algorithmName,
            type: type,
            averageTimeInMs: averageTimeInMs,
            memoryUsed: memoryUsed,
            repetitions: repetitions
        );

        Console.WriteLine($"Algorithm: {algorithmName}");
        Console.WriteLine($"Average Time: {averageTimeInMs} ms (over {repetitions} repetitions)");
        Console.WriteLine($"Memory Used: {memoryUsed / 1024} KB or {memoryUsed} bits");
        Console.WriteLine("---------------------------------");
    }
}
