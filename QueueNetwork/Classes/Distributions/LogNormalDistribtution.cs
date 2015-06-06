using System;

namespace QueueNetwork {
	public class LogNormalDistribution : DistributionInterface {
		private readonly double mu, sigma;
		private NormalDistribution random;

		public LogNormalDistribution(double mu, double sigma, int fixedSeed) {
			this.mu = mu;
			this.sigma = sigma;

			random = new NormalDistribution (0.0, 1.0, fixedSeed);
		}

		public LogNormalDistribution(double mu, double sigma) {
			this.mu = mu;
			this.sigma = sigma;

			random = new NormalDistribution (0.0, 1.0);
		}

		public double NextDouble () {
			return Math.Exp (mu + sigma * random.NextDouble ());
		}

		public double Expectation () {
			return Math.Exp(mu);
		}

		public double Cdf(double x) {
			return .5 + (.5 * Constants.Erf ((Math.Log(x) - mu) / (sigma * Math.Sqrt (2.0))));
		}

		public override string ToString () {
			return string.Format("LogN({0:##.00}, {1:##.00000}²)", mu, sigma);
		}

		public static LogNormalDistribution FromMuSigma(double mu, double sigma) {
			double 
			mu2 = mu*mu,
			mu4 = mu2*mu2;

			return new LogNormalDistribution (
				Math.Log(mu),
				Math.Sqrt((mu2 + Math.Sqrt(mu4 + 4 * mu2 * sigma)) / (2 * mu2))
			);
		}

		public static LogNormalDistribution FromMuSigma(double mu, double sigma, int fixedSeed) {
			double 
			mu2 = mu*mu,
			mu4 = mu2*mu2;

			return new LogNormalDistribution (
				Math.Log(mu),
				Math.Sqrt(Math.Log((mu2 + Math.Sqrt(mu4 + 4 * mu2 * sigma)) / (2 * mu2))),
				fixedSeed
			);
		}
	}
}

