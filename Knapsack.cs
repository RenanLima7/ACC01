namespace ACC01
{
    public class Knapsack
    {
        public int[] Weights { get; set; }
        public int[] Values { get; set; }
        public int Capacity { get; set; }

        public Knapsack(int[] weights, int[] values, int capacity)
        {
            if (weights.Length != values.Length)
            {
                throw new ArgumentException("Weights and values must have the same length.");
            }
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than zero.");
            }

            Weights = weights;
            Values = values;
            Capacity = capacity;
        }
    }
}
