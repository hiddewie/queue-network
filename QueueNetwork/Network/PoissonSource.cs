using System;
using System.Collections.Generic;

using QueueNetwork;

namespace QueueNetwork {
	public class PoissonSource : DistributionSource {
		public PoissonSource (double lambda) : base (new ExponentialDistribution (lambda)) {
		}
	}
}

