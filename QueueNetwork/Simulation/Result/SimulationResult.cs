using System;
using System.Collections.Generic;

namespace QueueNetwork.Simulation.Result {
	public abstract class SimulationResult : Dictionary<String, double> {
		/** ADD **/
		public static SimulationResult operator + (SimulationResult c1, SimulationResult c2) {
			SimulationResult res = new SimulationResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] + c2 [key];
			}
			return res;
		}

		public SimulationResult Add (SimulationResult c2) {
			foreach (String key in c2.Keys) {
				this[key] += c2 [key];
			}
			return this;
		}

		/** SUBTRACT **/
		public static SimulationResult operator - (SimulationResult c1, SimulationResult c2) {
			SimulationResult res = new SimulationResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] - c2 [key];
			}
			return res;
		}

		public SimulationResult Subtract (SimulationResult c2) {
			foreach (String key in c2.Keys) {
				this[key] -= c2 [key];
			}
			return this;
		}

		/** MULTIPLY **/
		public static SimulationResult operator * (SimulationResult c1, SimulationResult c2) {
			SimulationResult res = new SimulationResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] * c2 [key];
			}
			return res;
		}

		public static SimulationResult operator * (double a, SimulationResult c2) {
			SimulationResult res = new SimulationResult ();
			foreach (String key in c2.Keys) {
				res [key] = a * c2 [key];
			}
			return res;
		}

		public SimulationResult Multiply (SimulationResult c2) {
			foreach (String key in c2.Keys) {
				this[key] *= c2 [key];
			}
			return this;
		}

		public SimulationResult Multiply (double a) {
			foreach (String key in this.Keys) {
				this[key] *= a;
			}
			return this;
		}

		/** DIVIDE **/
		public static SimulationResult operator / (SimulationResult c1, double d) {
			SimulationResult res = new SimulationResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] / d;
			}
			return res;
		}

		public static SimulationResult operator / (SimulationResult c1, SimulationResult c2) {
			SimulationResult res = new SimulationResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] / c2 [key];
			}
			return res;
		}

		public SimulationResult Divide (SimulationResult c2) {
			foreach (String key in c2.Keys) {
				this[key] /= c2 [key];
			}
			return this;
		}

		public SimulationResult Divide (double d) {
			foreach (String key in this.Keys) {
				this[key] /= this [key];
			}
			return this;
		}

		public SimulationResult Sqrt () {
			foreach (String key in this.Keys) {
				this [key] = Math.Sqrt (this [key]);
			}
			return this;
		}
	}
}

