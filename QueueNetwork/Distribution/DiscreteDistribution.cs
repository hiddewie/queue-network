using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class DiscreteDistribution : IDistribution<int> {
		private double[] p;
		private UniformDistribution distribution;

		public DiscreteDistribution (double[] p) {
			this.p = p;
			this.distribution = new UniformDistribution (0, 1);
		}

		public DiscreteDistribution (double[] p, int fixedSeed) {
			this.p = p;
			this.distribution = new UniformDistribution (0, 1, fixedSeed);
		}

		public int NextRandom () {
			double random = distribution.NextRandom ();
			double d = 0.0;
			int i = 0;
			while (random > d + p [i]) {
				d += p [i];
				i += 1;
			}
			return i;
		}

		public double Expectation () {
			double e = 0.0;
			for (int i = 0; i < p.Length; i++) {
				e += (double)i * p [i];
			}
			return e / p.Length;
		}

		public double Cdf (int x) {
			double s = 0;
			for (int i = 0; i <= Math.Min (x, p.Length - 1); i++) {
				s += p [x];
			}
			return s;
		}

		public override string ToString () {
			return string.Format ("Disc(0..{0})", p.Length);
		}
	}
}

