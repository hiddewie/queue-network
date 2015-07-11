using System;
using System.Collections.Generic;

using QueueNetwork.Distibution;

namespace QueueNetwork {
	public class PoissonSource : DistributionSource {
		public PoissonSource (double lambda) : base (new ExponentialDistribution(lambda)) {
		}
	}
}

