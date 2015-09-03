using System;

namespace QueueNetwork {
	public class UniformDistribution : IDistribution<double> {
		private static long seed = DateTime.Now.Ticks;

		private readonly double min, max;
		private Random random;

		public UniformDistribution (double min, double max, int fixedSeed) {
			this.min = min;
			this.max = max;

			random = new Random ((int)fixedSeed);
		}

		public UniformDistribution (double min, double max) {
			this.min = min;
			this.max = max;

			seed++;
			random = new Random ((int)seed);
		}

		public double NextRandom () {
			return min + random.NextDouble () * (max - min);
		}

		public double Expectation () {
			return (max + min) / 2.0;
		}

		public double Cdf (double x) {
			if (x < min) {
				return 0.0;
			} else if (x > max) {
				return 1.0;
			} else {
				return (x - min) / (max - min);
			}
		}

		public override string ToString () {
			return string.Format ("U({0:##.00}, {1:##.00})", min, max);
		}
	}
}

