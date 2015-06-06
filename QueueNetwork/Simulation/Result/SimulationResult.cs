using System;

namespace QueueNetwork.Simulation.Result {
	public class SimulationResult {
		public SimulationResult () : this(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0) {
		}

		public SimulationResult (double ODelayTime, double RDelayTime, double OServiceTime, double RServiceTime, double TotalServiceTime, double redirections, double overTime, double OOOperationPercentage, double ORDistribution) {
			this.ODelayTime = ODelayTime;
			this.RDelayTime = RDelayTime;
			this.OServiceTime = OServiceTime;
			this.RServiceTime = RServiceTime;
			this.TotalServiceTime = TotalServiceTime;
			this.Redirections = redirections;
			this.OverTime = overTime;
			this.OOOperationPercentage = OOOperationPercentage;
			this.ORDistribution = ORDistribution;
		}

		public double ODelayTime { get; set; }
		public double RDelayTime { get; set; }
		public double OServiceTime { get; set; }
		public double RServiceTime { get; set; }
		public double TotalServiceTime { get; set; }
		public double Redirections { get; set; }
		public double OverTime { get; set; }
		public double OOOperationPercentage { get; set; }
		public double ORDistribution { get; set; }

		public static SimulationResult operator + (SimulationResult c1, SimulationResult c2) {
			return new SimulationResult (
				c1.ODelayTime + c2.ODelayTime,
				c1.RDelayTime + c2.RDelayTime,
				c1.OServiceTime + c2.OServiceTime,
				c1.RServiceTime + c2.RServiceTime,
				c1.TotalServiceTime + c2.TotalServiceTime,
				c1.Redirections + c2.Redirections,
				c1.OverTime + c2.OverTime,
				c1.OOOperationPercentage + c2.OOOperationPercentage,
				c1.ORDistribution + c2.ORDistribution
			);
		}

		public SimulationResult Add (SimulationResult c2) {
			ODelayTime += c2.ODelayTime;
			RDelayTime += c2.RDelayTime;
			OServiceTime += c2.OServiceTime;
			RServiceTime += c2.RServiceTime;
			TotalServiceTime += c2.TotalServiceTime;
			Redirections += c2.Redirections;
			OverTime += c2.OverTime;
			OOOperationPercentage += c2.OOOperationPercentage;
			ORDistribution += c2.ORDistribution;
			return this;
		}

		public static SimulationResult operator - (SimulationResult c1, SimulationResult c2) {
			return new SimulationResult (
				c1.ODelayTime - c2.ODelayTime,
				c1.RDelayTime - c2.RDelayTime,
				c1.OServiceTime - c2.OServiceTime,
				c1.RServiceTime - c2.RServiceTime,
				c1.TotalServiceTime - c2.TotalServiceTime,
				c1.Redirections - c2.Redirections,
				c1.OverTime - c2.OverTime,
				c1.OOOperationPercentage - c2.OOOperationPercentage,
				c1.ORDistribution - c2.ORDistribution
			);
		}

		public SimulationResult Subtract (SimulationResult c2) {
			ODelayTime -= c2.ODelayTime;
			RDelayTime -= c2.RDelayTime;
			OServiceTime -= c2.OServiceTime;
			RServiceTime -= c2.RServiceTime;
			TotalServiceTime -= c2.TotalServiceTime;
			Redirections -= c2.Redirections;
			OverTime -= c2.OverTime;
			OOOperationPercentage -= c2.OOOperationPercentage;
			ORDistribution -= c2.ORDistribution;
			return this;
		}

		public static SimulationResult operator * (double a, SimulationResult c2) {
			return new SimulationResult (
				a * c2.ODelayTime,
				a * c2.RDelayTime,
				a * c2.OServiceTime,
				a * c2.RServiceTime,
				a * c2.TotalServiceTime,
				a * c2.Redirections,
				a * c2.OverTime,
				a * c2.OOOperationPercentage,
				a * c2.ORDistribution
			);
		}

		public static SimulationResult operator * (SimulationResult c1, SimulationResult c2) {
			return new SimulationResult (
				c1.ODelayTime * c2.ODelayTime,
				c1.RDelayTime * c2.RDelayTime,
				c1.OServiceTime * c2.OServiceTime,
				c1.RServiceTime * c2.RServiceTime,
				c1.TotalServiceTime * c2.TotalServiceTime,
				c1.Redirections * c2.Redirections,
				c1.OverTime * c2.OverTime,
				c1.OOOperationPercentage * c2.OOOperationPercentage,
				c1.ORDistribution * c2.ORDistribution
			);
		}

		public SimulationResult Multiply (SimulationResult c2) {
			ODelayTime *= c2.ODelayTime;
			RDelayTime *= c2.RDelayTime;
			OServiceTime *= c2.OServiceTime;
			RServiceTime *= c2.RServiceTime;
			TotalServiceTime *= c2.TotalServiceTime;
			Redirections *= c2.Redirections;
			OverTime *= c2.OverTime;
			OOOperationPercentage *= c2.OOOperationPercentage;
			ORDistribution *= c2.ORDistribution;
			return this;
		}

		public SimulationResult Multiply (double a) {
			ODelayTime *= a;
			RDelayTime *= a;
			OServiceTime *= a;
			RServiceTime *= a;
			TotalServiceTime *= a;
			Redirections *= a;
			OverTime *= a;
			OOOperationPercentage *= a;
			ORDistribution *= a;
			return this;
		}

		public static SimulationResult operator / (SimulationResult c1, double d) {
			return new SimulationResult (
				c1.ODelayTime / d,
				c1.RDelayTime / d,
				c1.OServiceTime / d,
				c1.RServiceTime / d,
				c1.TotalServiceTime / d,
				c1.Redirections / d,
				c1.OverTime / d,
				c1.OOOperationPercentage / d,
				c1.ORDistribution / d
			);
		}

		public static SimulationResult operator / (SimulationResult c1, SimulationResult c2) {
			return new SimulationResult (
				c1.ODelayTime / c2.ODelayTime,
				c1.RDelayTime / c2.RDelayTime,
				c1.OServiceTime / c2.OServiceTime,
				c1.RServiceTime / c2.RServiceTime,
				c1.TotalServiceTime / c2.TotalServiceTime,
				c1.Redirections / c2.Redirections,
				c1.OverTime / c2.OverTime,
				c1.OOOperationPercentage / c2.OOOperationPercentage,
				c1.ORDistribution / c2.ORDistribution
			);
		}

		public SimulationResult Divide (SimulationResult c2) {
			ODelayTime /= c2.ODelayTime;
			RDelayTime /= c2.RDelayTime;
			OServiceTime /= c2.OServiceTime;
			RServiceTime /= c2.RServiceTime;
			TotalServiceTime /= c2.TotalServiceTime;
			Redirections /= c2.Redirections;
			OverTime /= c2.OverTime;
			OOOperationPercentage /= c2.OOOperationPercentage;
			ORDistribution /= c2.ORDistribution;
			return this;
		}

		public SimulationResult Divide (double d) {
			ODelayTime /= d;
			RDelayTime /= d;
			OServiceTime /= d;
			RServiceTime /= d;
			TotalServiceTime /= d;
			Redirections /= d;
			OverTime /= d;
			OOOperationPercentage /= d;
			ORDistribution /= d;
			return this;
		}

		public SimulationResult Sqrt () {
			return new SimulationResult (
				Math.Sqrt(ODelayTime),
				Math.Sqrt(RDelayTime),
				Math.Sqrt(OServiceTime),
				Math.Sqrt(RServiceTime),
				Math.Sqrt(TotalServiceTime),
				Math.Sqrt(Redirections),
				Math.Sqrt(OverTime),
				Math.Sqrt(OOOperationPercentage),
				Math.Sqrt(ORDistribution)
			);
		}

		public override string ToString () {
			return string.Format ("[SimulationResult: Delay: {0} + {1}, Redirections: {1}, OverTime: {2}]", ODelayTime, RDelayTime, Redirections, OverTime);
		}
	}
}

