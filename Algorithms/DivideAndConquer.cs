using ACC01.Algorithms.Interfaces;

namespace ACC01.Algorithms
{
    public class DivideAndConquer : IAlgorithm
    {
        public int Solve(Knapsack knapsack)
        {
            return SolveKnapsack(knapsack.Weights.Length - 1, knapsack.Capacity);

            int SolveKnapsack(int index, int remainingCapacity)
            {
                if (index < 0 || remainingCapacity <= 0)
                {
                    return 0;
                }

                if (knapsack.Weights[index] > remainingCapacity)
                {
                    return SolveKnapsack(index - 1, remainingCapacity);
                }

                int excludeItem = SolveKnapsack(index - 1, remainingCapacity);

                int includeItem = knapsack.Values[index] + SolveKnapsack(index - 1, remainingCapacity - knapsack.Weights[index]);

                return Math.Max(excludeItem, includeItem);
            }
        }
    }
}
