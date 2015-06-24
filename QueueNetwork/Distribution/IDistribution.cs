using System;

namespace QueueNetwork.Distibution {
	public interface IDistribution {
		double NextDouble ();
		double Expectation ();
		double Cdf(double x);
	}
}

