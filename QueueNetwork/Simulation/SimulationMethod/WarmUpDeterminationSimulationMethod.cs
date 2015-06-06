using System;
using System.Collections.Generic;

using QueueNetwork.Simulation.Result;

namespace QueueNetwork.Simulation.SimulationMethod {
	public class WarmUpDeterminationSimulationMethod /*: SimulationMethod*/ {
		private int numDelaysRequired, numReplicationsRequired, determinationInterval, w;
		private int numReplications = 0;
		private double[,] results;
		private int foundL = 0;

		public WarmUpDeterminationSimulationMethod(int numDelaysRequired, int numReplicationsRequired, int determinationInterval, int w) {
			this.numDelaysRequired = numDelaysRequired;
			this.numReplicationsRequired = numReplicationsRequired;
			this.determinationInterval = determinationInterval;
			this.w = w;

			results = new double[numReplicationsRequired, numDelaysRequired];
		}

		public double GetFixedTiming () {
			return Constants.INF;
		}

		public bool ContinueSimulation () {
			return numReplications < numReplicationsRequired;
		}

		public bool ContinueRun (double simTime, int numCustomersDelayed, int numRegenerations) {
			return numCustomersDelayed < numDelaysRequired;
		}

		public void Arrive (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue) {

		}

		public void Depart (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue, Queue<double> timeArrival) {
			if (numberInQueue > 0) {
				results [numReplications, numCustomersDelayed] = (simTime - timeArrival.Peek ());
			} else {
				results [numReplications, numCustomersDelayed] = 0.0;
			}
		}

		public void FixedTiming (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue) {

		}

		public void RunStarted () {

		}

		public void RunFinished (double simTime, int numCustomersDelayed) {
			numReplications++;
		}

		public void Process (double simTime, double timeSinceLastProcess, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue, int numRegenerations) {

		}

		public Interval<SimulationResult> GetResult (double confidenceIntervalPercentage) {
			int n = numReplicationsRequired, m = numDelaysRequired;

			double[,] averageResults = new double[n, m];
			for (int i = 0; i < n; i++) {
				averageResults [i, 0] = results[i, 0];
				for (int j = 1; j < m; j++) {
					averageResults [i, j] = averageResults [i, j - 1] + results [i, j];
				}
				for (int j = 1; j < m; j++) {
					averageResults [i, j] /= (j + 1);
				}
			}

			double[] average = new double[m];

			for (int i = 0; i < m; i++) {
				average[i] = 0.0;
				for (int j = 0; j < n; j++) {
					average[i] += averageResults[j, i];
				}
				average[i] /= n;
			}

			double[] averageW = new double[m];

			for (int i = 0; i < w; i++) {
				averageW [i] = 0.0;
				for (int s = 0; s < 2 * i - 1; s++) {
					averageW [i] += average [s];
				}
				averageW[i] /= (2 * i - 1);

				if (determinationInterval > 0 && (i % determinationInterval) == determinationInterval - 1) {
					Console.WriteLine ("{0}", averageW [i]);
				}
			}
			for (int i = w; i < m - w; i++) {
				averageW [i] = 0.0;
				for (int s = i - w; s <= i + w; s++) {
					averageW [i] += average [s];
				}
				averageW[i] /= (2 * w + 1);

				if (determinationInterval > 0 && (i % determinationInterval) == determinationInterval - 1) {
					Console.WriteLine ("{0}", averageW [i]);
				}
			}

			double quotient;
			double margin = 0.99;

			for (int i = 0; i < m / 2; i++) {
				quotient = averageW [i] / averageW [2 * i];
				if (quotient > margin && quotient < (1 / margin)) {
					foundL = i;
					if (determinationInterval > 0) {
						Console.WriteLine (i + " " + (i / determinationInterval));
					}
					break;
				}
			}

			return null;
		}

		public int GetL() {
			return foundL;
		}

		public override string ToString () {
			return string.Format("WarmingUpDetermination ({0} replications, {1} delays, determinationInterval = {2}, w = {3})", numReplicationsRequired, numDelaysRequired, determinationInterval, w);
		}
	}
}

