using System;

namespace QueueNetwork {
	public interface DistributionInterface {
		double NextDouble ();
		double Expectation ();
		double Cdf(double x);
	}
}

