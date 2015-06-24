using System;

namespace QueueNetwork.Distibution {
	public class DeterministicDistribution : IDistribution<double> {
		private readonly double lambda;

		public DeterministicDistribution(double lambda) {
			this.lambda = lambda;
		}

		public double NextRandom () {
			return 1.0 / lambda;
		}

		public double Expectation () {
			return 1.0 / lambda;
		}

		public double Cdf(double x) {
			double e = Expectation ();
			if (x < e) {
				return 0.0;
			} else if (x == e) {
				return  0.5;
			} else {
				return 1.0;
			}
		}

		public override string ToString () {
			return string.Format("D({0:##.00})", lambda);
		}
	}
}

