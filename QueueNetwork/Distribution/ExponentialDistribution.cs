using System;

namespace QueueNetwork {
	public class ExponentialDistribution : IDistribution<double> {
		private static long seed = DateTime.Now.Ticks;

		private readonly double lambda;
		private Random random;

		public ExponentialDistribution (double lambda, int fixedSeed) {
			this.lambda = lambda;
			random = new Random ((int)fixedSeed);
		}

		public ExponentialDistribution (double lambda) {
			this.lambda = lambda;

			seed++;
			random = new Random ((int)seed);
		}

		public double NextRandom () {
			return -Math.Log (random.NextDouble ()) / lambda;
		}

		public double Expectation () {
			return 1.0 / lambda;
		}

		public double Cdf (double x) {
			return 1.0 - Math.Exp (-lambda * x);
		}

		public override string ToString () {
			return string.Format ("E({0:##.00})", lambda);
		}
	}
}

