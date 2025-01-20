namespace Domain.Core
{
    public class WeightValue<T>
    {
        public double Weight { get; private set; }

        public T Value { get; private set; }

        public WeightValue(double Weight, T Value)
        {
            this.Weight = Weight;
            this.Value = Value;
        }
    }

    static public class WeightValueExtensions
    {
        static private readonly Random _random = new Random();

        static public T Pick<T>(this List<WeightValue<T>> weightValues)
        {
            if (weightValues.Count == 0)
            {
                throw new ArgumentException("La lista de randomizables no puede estar vacía.");
            }

            double totalProbability = weightValues.Sum(r => r.Weight);

            if (totalProbability == 0)
            {
                throw new InvalidOperationException("La suma de probabilidades no puede ser cero.");
            }

            double randomValue = _random.NextDouble() * totalProbability;

            double cumulativeProbability = 0;

            foreach (var randomizable in weightValues)
            {
                cumulativeProbability += randomizable.Weight;

                if (randomValue <= cumulativeProbability)
                {
                    return randomizable.Value; // Devuelve el valor correspondiente
                }
            }

            throw new InvalidOperationException("No se pudo seleccionar un elemento aleatorio.");
        }
    }
}