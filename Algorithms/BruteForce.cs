using ACC01.Algorithms.Interfaces;

namespace ACC01.Algorithms
{
    public class BruteForce : IAlgorithm
    {
        public int Solve(Knapsack knapsack)
        {
            int n = knapsack.Weights.Length;
            return KnapsackRecursive(n - 1, knapsack.Capacity);

            int KnapsackRecursive(int i, int remainingCapacity)
            {
                if (i < 0 || remainingCapacity <= 0)
                    return 0;

                if (knapsack.Weights[i] > remainingCapacity)
                    return KnapsackRecursive(i - 1, remainingCapacity);

                int excludeItem = KnapsackRecursive(i - 1, remainingCapacity);
                int includeItem = knapsack.Values[i] + KnapsackRecursive(i - 1, remainingCapacity - knapsack.Weights[i]);

                return Math.Max(excludeItem, includeItem);
            }
        }
    }
}
