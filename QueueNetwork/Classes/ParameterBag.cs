using System;

namespace QueueNetwork {
	public class ParameterBag {
		public ParameterBag (
			double lambdaO,
			double lambdaR,
			double OMin,
			double OMax,
			double nu,
			double mu,
			double sigma,
			double p,
			int N, 
			int n) {

			this.LambdaO = lambdaO;
			this.LambdaR = lambdaR;
			this.OMin = OMin;
			this.OMax = OMax;
			this.Nu = nu;
			this.Mu = mu;
			this.Sigma = sigma;
			this.P = p;
			this.N = N;
			this.SmallN = n;
		}

		public double LambdaO { get; set; }
		public double LambdaR { get; set; }
		public double OMin { get; set; }
		public double OMax { get; set; }
		public double Nu { get; set; }
		public double Mu { get; set; }
		public double Sigma { get; set; }
		public double P { get; set; }
		public int N { get; set; }
		public int SmallN { get; set; }
	}
}

