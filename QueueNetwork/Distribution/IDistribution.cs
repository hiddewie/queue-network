using System;

namespace QueueNetwork.Distibution {
	public interface IDistribution<T> {
		T NextRandom ();
		double Expectation ();
		double Cdf(T x);
	}
}

