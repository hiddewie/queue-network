using System;

namespace QueueNetwork.Simulation.Distibution {
	public interface IDistribution {
		double NextDouble ();
		double Expectation ();
		double Cdf(double x);
	}
}

