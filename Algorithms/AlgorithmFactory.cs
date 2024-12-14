using ACC01.Algorithms.Interfaces;

namespace ACC01.Algorithms
{
    public enum AlgorithmType
    {
        BruteForce = 0,
        DynamicProgramming = 1,
        DivideAndConquer = 2
    }

    public class AlgorithmFactory
    {
        public static IAlgorithm GetAlgorithm(AlgorithmType type)
        {
            return type switch
            {
                AlgorithmType.BruteForce => new BruteForce(),
                AlgorithmType.DynamicProgramming => new DynamicProgramming(),
                AlgorithmType.DivideAndConquer => new DivideAndConquer(),
                _ => throw new ArgumentException("Invalid algorithm type"),
            };
        }
    }
}
