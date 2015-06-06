using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class RegenerativeMethod/* : SimulationMethod */{
		private int numRegenerationBatches, regenerationsPerBatch;

		private bool continueSimulation = true;
		private int regenerationIndex = 0, regenerationNumCustomers;
		private double regenerationSimTime;
		private List<Quotient<SimulationResult>> results = new List<Quotient<SimulationResult>> ();
		private int N;

		private double totalDelays, areaNumInQueue, areaServerStatus;

		public RegenerativeMethod (int N, int numRegenerationBatches, int regenerationsPerBatch) {
			this.N = N;
			this.numRegenerationBatches = numRegenerationBatches;
			this.regenerationsPerBatch = regenerationsPerBatch;
		}

		public double GetFixedTiming () {
			return Constants.INF;
		}

		public bool ContinueSimulation () {
			bool ret = continueSimulation;
			continueSimulation = false;
			return ret;
		}

		public bool ContinueRun (double simTime, int numCustomersDelayed, int numRegenerations) {
			return numRegenerations < numRegenerationBatches * regenerationsPerBatch;
		}

		public void Arrive (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue) {

		}

		public void Depart (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue, Queue<double> timeArrival) {
			if (numberInQueue > 0) {
				totalDelays += (simTime - timeArrival.Peek ());
			}
		}


		public void FixedTiming (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue) {

		}

		public void RunStarted () {

		}

		public void RunFinished (double simTime, int numCustomersDelayed) {

		}

		public void Process (double simTime, double timeSinceLastProcess, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue, int numRegenerations) {
			areaNumInQueue += (numberInQueue * timeSinceLastProcess);
			for (int i = 0; i < N; i++) {
				if (serverStatus[i] == ServerStatus.BUSY) {
					areaServerStatus += timeSinceLastProcess;
				}
			}

			if (numRegenerations != regenerationIndex) {
				if (regenerationIndex > 0) {

					/*Quotient<SimulationResult> result = new Quotient<SimulationResult> (
						new SimulationResult(0.0, areaNumInQueue, totalDelays, areaServerStatus),
						new SimulationResult(0.0, (simTime - regenerationSimTime), (numCustomersDelayed - regenerationNumCustomers), (simTime - regenerationSimTime))
					);
					results.Add (result);*/
				}

				regenerationIndex++;
				totalDelays = 0.0;
				areaNumInQueue = 0.0;
				areaServerStatus = 0.0;
				regenerationNumCustomers = numCustomersDelayed;
				regenerationSimTime = simTime;
			}
		}

		public Interval<SimulationResult> GetResult (double confidenceIntervalPercentage) {
			return ConfidenceInterval.CreateInterval (results, confidenceIntervalPercentage);
		}

		public override string ToString () {
			return string.Format("Regenerative ({0}, {1})", numRegenerationBatches, regenerationsPerBatch);
		}
	}
}

