using System;

namespace QueueNetwork {
	public interface IDistribution<T> {
		T NextRandom ();

		double Expectation ();

		double Cdf (T x);
	}
}

