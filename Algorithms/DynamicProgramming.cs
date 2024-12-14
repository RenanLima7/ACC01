using ACC01.Algorithms.Interfaces;

namespace ACC01.Algorithms
{
    public class DynamicProgramming : IAlgorithm
    {
        public int Solve(Knapsack knapsack)
        {
            int n = knapsack.Weights.Length;
            int[,] dp = new int[n + 1, knapsack.Capacity + 1];

            for (int i = 1; i <= n; i++)
            {
                for (int w = 1; w <= knapsack.Capacity; w++)
                {
                    if (knapsack.Weights[i - 1] <= w)
                    {
                        dp[i, w] = Math.Max(
                            dp[i - 1, w],
                            knapsack.Values[i - 1] + dp[i - 1, w - knapsack.Weights[i - 1]]
                        );
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            return dp[n, knapsack.Capacity];
        }
    }
}
