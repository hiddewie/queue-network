using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class SimulationResult : Dictionary<String, double> {

		public ResultFactory Factory {
			get;
			set;
		}

		/** ADD **/
		public static SimulationResult operator + (SimulationResult c1, SimulationResult c2) {
			SimulationResult res = c1.Factory.CreateResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] + c2 [key];
			}
			return res;
		}

		public SimulationResult Add (SimulationResult c2) {
			foreach (String key in c2.Keys) {
				this [key] += c2 [key];
			}
			return this;
		}

		/** SUBTRACT **/
		public static SimulationResult operator - (SimulationResult c1, SimulationResult c2) {
			SimulationResult res = c1.Factory.CreateResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] - c2 [key];
			}
			return res;
		}

		public SimulationResult Subtract (SimulationResult c2) {
			foreach (String key in c2.Keys) {
				this [key] -= c2 [key];
			}
			return this;
		}

		/** MULTIPLY **/
		public static SimulationResult operator * (SimulationResult c1, SimulationResult c2) {
			SimulationResult res = c1.Factory.CreateResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] * c2 [key];
			}
			return res;
		}

		public static SimulationResult operator * (double a, SimulationResult c2) {
			SimulationResult res = c2.Factory.CreateResult ();
			foreach (String key in c2.Keys) {
				res [key] = a * c2 [key];
			}
			return res;
		}

		public SimulationResult Multiply (SimulationResult c2) {
			foreach (String key in c2.Keys) {
				this [key] *= c2 [key];
			}
			return this;
		}

		public SimulationResult Multiply (double a) {
			foreach (String key in this.Keys) {
				this [key] *= a;
			}
			return this;
		}

		/** DIVIDE **/
		public static SimulationResult operator / (SimulationResult c1, double d) {
			SimulationResult res = c1.Factory.CreateResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] / d;
			}
			return res;
		}

		public static SimulationResult operator / (SimulationResult c1, SimulationResult c2) {
			SimulationResult res = c1.Factory.CreateResult ();
			foreach (String key in c1.Keys) {
				res [key] = c1 [key] / c2 [key];
			}
			return res;
		}

		public SimulationResult Divide (SimulationResult c2) {
			foreach (String key in c2.Keys) {
				this [key] /= c2 [key];
			}
			return this;
		}

		public SimulationResult Divide (double d) {
			foreach (String key in this.Keys) {
				this [key] /= this [key];
			}
			return this;
		}

		public SimulationResult Sqrt () {
			foreach (String key in this.Keys) {
				this [key] = Math.Sqrt (this [key]);
			}
			return this;
		}

		public static readonly double studentT = 1.95996635692678;

		public static Interval<SimulationResult> CreateInterval (List<SimulationResult> results, double confidencePercentage, ResultFactory resultFactory) {
			int n = results.Count;

			if (n < 2) {
				throw new Exception (String.Format ("Cannot create interval of less than 2 results, got {0}", n));
			}

			SimulationResult average = resultFactory.CreateResult ();
			for (int i = 0; i < n; i++) {
				average.Add (results [i]);
			}
			average.Divide (n);
			//Console.WriteLine (average);

			SimulationResult deviation = resultFactory.CreateResult ();
			for (int i = 0; i < n; i++) {
				deviation.Add ((results [i] - average) * (results [i] - average));
			}
			deviation.Divide (n - 1);
			//Console.WriteLine (deviation);

			return new Interval<SimulationResult> (
				average - studentT * (deviation / n).Sqrt (),
				average + studentT * (deviation / n).Sqrt ()
			);
		}

		public static Interval<T> CreateInterval<T> (List<SimulationResult> results1, List<SimulationResult> results2, double confidencePercentage) where T : SimulationResult, new() {
			int n = results1.Count;
			if (n != results2.Count) {
				throw new Exception (string.Format ("Result counts do not match ({0} != {1})", n, results2.Count));
			}

			if (n < 2) {
				throw new Exception (String.Format ("Cannot create interval of less than 2 results, got {0}", n));
			}

			T[] difference = new T[n];
			for (int i = 0; i < n; i++) {
				difference [i] = (results1 [i] - results2 [i]) as T;
			}

			T average = new T ();
			for (int i = 0; i < n; i++) {
				average.Add (difference [i]);
			}
			average.Divide (n);

			T deviation = new T ();
			for (int i = 0; i < n; i++) {
				deviation.Add ((difference [i] - average) * (difference [i] - average));
			}
			deviation.Divide (n - 1);

			return new Interval<T> (
				average - studentT * (deviation / n).Sqrt () as T,
				average + studentT * (deviation / n).Sqrt () as T
			);
		}

		public static Interval<T> CreateInterval<T> (List<Quotient<SimulationResult>> results, double confidencePercentage) where T : SimulationResult, new() {
			int n = results.Count;

			if (n < 2) {
				return new Interval<T> (new T (), new T ());
			}

			T Z = new T ();
			for (int i = 0; i < n; i++) {
				Z = Z + results [i].Numerator as T;
			}
			Z = Z / n as T;

			T N = new T ();
			for (int i = 0; i < n; i++) {
				N = N + results [i].Denominator as T;
			}
			N = N / n as T;

			T average = Z / N as T;

			T[,] sigma = new T[2, 2];
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					sigma [i, j] = new T ();
				}
			}
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < 2; j++) {
					for (int k = 0; k < 2; k++) {
						sigma [j, k] = sigma [j, k] + ( 
						    (j == 0 ? results [i].Numerator - Z : results [i].Denominator - N) *
						    (k == 0 ? results [i].Numerator - Z : results [i].Denominator - N)
						) as T;
					}
				}
			}
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					sigma [i, j] = sigma [i, j] / (n - 1) as T;
				}
			}

			SimulationResult deviation = sigma [0, 0] - 2 * average * sigma [0, 1] + average * average * sigma [1, 1];

			return new Interval<T> (
				average - studentT * (deviation / n).Sqrt () / N as T,
				average + studentT * (deviation / n).Sqrt () / N as T
			);
		}
	}
}

