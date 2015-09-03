using System;

namespace QueueNetwork {
	public class UniformDiscreteDistribution : IDistribution<int> {
		private int min, max;
		private UniformDistribution distribution;

		public UniformDiscreteDistribution (int min, int max, int fixedSeed) {
			if (min > max) {
				throw new Exception (String.Format("Min value ({0}) is greater than max value ({1})", min, max));
			}
			this.min = min;
			this.max = max;
			distribution = new UniformDistribution (0, 1, fixedSeed);
		}

		public UniformDiscreteDistribution (int min, int max) {
			if (min > max) {
				throw new Exception (String.Format("Min value ({0}) is greater than max value ({1})", min, max));
			}
			this.min = min;
			this.max = max;
			distribution = new UniformDistribution (0, 1);
		}

		public int NextRandom () {
			double random = distribution.NextRandom ();
			return min + (int)(random * ((double)(max - min)));
		}

		public double Expectation () {
			return (double)(max - min) / 2.0;
		}

		public double Cdf (int x) {
			if (x < min) {
				return 0.0;
			} else if (x > max) {
				return 1.0;
			} else {
				if (min == max) {
					return 1.0;
				} else {
					return (double) (x - min) / (double) (max - min);
				}
			}
		}

		public override string ToString () {
			return string.Format("UDisc({0}..{1})", min, max);
		}
	}
}

