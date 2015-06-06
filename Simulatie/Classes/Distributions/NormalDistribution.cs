using System;

namespace QueueNetwork {
	public class NormalDistribution : DistributionInterface {
		private static long seed = DateTime.Now.Ticks;

		private readonly double mu, sigma;
		private Random random;
		private bool nextDoubleReady = false;
		private double nextDoubleCache;

		public NormalDistribution(double mu, double sigma, int fixedSeed) {
			this.mu = mu;
			this.sigma = sigma;

			random = new Random ((int) fixedSeed);
		}

		public NormalDistribution(double mu, double sigma) {
			this.mu = mu;
			this.sigma = sigma;

			seed++;
			random = new Random ((int) seed);
		}

		public double NextDouble () {
			if (nextDoubleReady) {
				nextDoubleReady = false;
				return mu + sigma * nextDoubleCache;
			} else {
				double u1, u2, v1, v2, s, q;

				do {
					u1 = random.NextDouble ();
					u2 = random.NextDouble ();
					v1 = 2 * u1 - 1;
					v2 = 2 * u2 - 1;
					s = v1 * v1 + v2 * v2;
				} while (s > 1.0 || s == 0.0);

				nextDoubleReady = true;
				q = Math.Sqrt(-2 * Math.Log(s) / s);
				nextDoubleCache = q * v1;
				return mu + sigma * q * v2;
			}
		}

		public double Expectation () {
			return mu;
		}

		public double Cdf(double x) {
			return .5 * (1.0 + Constants.Erf ((x - mu) / (sigma * Math.Sqrt (2.0))));
		}

		public override string ToString () {
			return string.Format("N({0:##.00}, {1:##.00}²)", mu, sigma);
		}
	}
}

