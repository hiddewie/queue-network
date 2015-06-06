using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class ConfidenceInterval {
		public static readonly double studentT = 1.95996635692678;

		public static Interval<SimulationResult> CreateInterval (List<SimulationResult> results, double confidencePercentage) {
			int n = results.Count;

			if (n < 2) {
				return new Interval<SimulationResult> (new SimulationResult(), new SimulationResult());
			}

			SimulationResult average = new SimulationResult();
			for (int i = 0; i < n; i++) {
				average.Add (results [i]);
			}
			average.Divide (n);
			//Console.WriteLine (average);

			SimulationResult deviation = new SimulationResult();
			for (int i = 0; i < n; i++) {
				deviation.Add ((results [i] - average) * (results [i] - average));
			}
			deviation.Divide(n - 1);
			//Console.WriteLine (deviation);

			return new Interval<SimulationResult>(
				average - studentT * (deviation / n).Sqrt(),
				average + studentT * (deviation / n).Sqrt()
			);
		}

		public static Interval<SimulationResult> CreateInterval (List<SimulationResult> results1, List<SimulationResult> results2, double confidencePercentage) {
			int n = results1.Count;
			if (n != results2.Count) {
				throw new Exception (string.Format("Result counts do not match ({0} != {1})", n, results2.Count));
			}

			if (n < 2) {
				return new Interval<SimulationResult> (new SimulationResult(), new SimulationResult());
			}

			SimulationResult[] difference = new SimulationResult[n];
			for (int i = 0; i < n; i++) {
				difference [i] = results1 [i] - results2 [i];
			}

			SimulationResult average = new SimulationResult();
			for (int i = 0; i < n; i++) {
				average.Add (difference [i]);
			}
			average.Divide (n);

			SimulationResult deviation = new SimulationResult();
			for (int i = 0; i < n; i++) {
				deviation.Add ((difference [i] - average) * (difference [i] - average));
			}
			deviation.Divide(n - 1);

			return new Interval<SimulationResult>(
				average - studentT * (deviation / n).Sqrt(),
				average + studentT * (deviation / n).Sqrt()
			);
		}

		public static Interval<SimulationResult> CreateInterval (List<Quotient<SimulationResult>> results, double confidencePercentage) {
			int n = results.Count;

			if (n < 2) {
				return new Interval<SimulationResult> (new SimulationResult(), new SimulationResult());
			}


			SimulationResult Z = new SimulationResult();
			for (int i = 0; i < n; i++) {
				Z += results [i].Numerator;
			}
			Z /= n;

			SimulationResult N = new SimulationResult();
			for (int i = 0; i < n; i++) {
				N += results [i].Denominator;
			}
			N /= n;

			SimulationResult average = Z / N;

			SimulationResult[,] sigma = new SimulationResult[2,2];
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					sigma [i, j] = new SimulationResult ();
				}
			}
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < 2; j++) {
					for (int k = 0; k < 2; k++) {
						sigma[j, k] += 
							(j == 0 ? results[i].Numerator - Z : results[i].Denominator - N) * 
							(k == 0 ? results[i].Numerator - Z : results[i].Denominator - N);
					}
				}
			}
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					sigma[i, j] /= (n - 1);
				}
			}

			SimulationResult deviation = sigma [0, 0] - 2 * average * sigma [0, 1] + average * average * sigma [1, 1];

			return new Interval<SimulationResult> (
				average - studentT * (deviation / n).Sqrt() / N,
				average + studentT * (deviation / n).Sqrt() / N
			);
		}
	}
}

