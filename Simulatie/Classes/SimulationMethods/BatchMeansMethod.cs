using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class BatchMeansMethod /*: SimulationMethod*/ {
		private double batchLength;
		private int numBatches, customersPerBatch, l;
		private bool continueSimulation = true;
		private int N;

		private SimulationResult[] results;

		private int queueLengthBatchIndex = 0, delayBatchIndex = 0;
		private double totalDelays, areaNumInQueue, areaServerStatus;

		public BatchMeansMethod(int N, int numBatches, double batchLength, int customersPerBatch, int l) {
			this.numBatches = numBatches;
			this.batchLength = batchLength;
			this.customersPerBatch = customersPerBatch;
			this.l = l;
			this.N = N;

			results = new SimulationResult[numBatches];
			for (int i = 0; i < numBatches; i++) {
				results [i] = new SimulationResult ();
			}
		}

		public double GetFixedTiming () {
			return batchLength;
		}

		public bool ContinueRun (double simTime, int numCustomersDelayed, int numRegenerations) {
			return simTime < (numBatches * batchLength) || numCustomersDelayed < (numBatches * customersPerBatch);
		}

		public bool ContinueSimulation () {
			bool ret = continueSimulation;
			continueSimulation = false;
			return ret;
		}

		public void Arrive (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue) {

		}

		public void Depart (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue, Queue<double> timeArrival) {
			if (numberInQueue > 0) {
				totalDelays += (simTime - timeArrival.Peek ());
			}
			if ((numCustomersDelayed % customersPerBatch) == (customersPerBatch - 1)) { // Last customer leaves of a batch
				if (delayBatchIndex < numBatches) {
					//results [delayBatchIndex].DelayTime = totalDelays / customersPerBatch;
					delayBatchIndex++;

					totalDelays = 0.0;
				}
			}
		}


		public void FixedTiming (double simTime, int numCustomersDelayed, ServerStatus[] serverStatus, int numberInQueue) {
			if (queueLengthBatchIndex < numBatches) {
				//results [queueLengthBatchIndex].QueueLength = areaNumInQueue / batchLength;
				//results [queueLengthBatchIndex].ServerUtilisation = areaServerStatus / batchLength;
				queueLengthBatchIndex++;

				areaNumInQueue = 0.0;
				areaServerStatus = 0.0;
			}
		}

		public void RunStarted () {
			totalDelays = 0.0;
			areaNumInQueue = 0.0;
			areaServerStatus = 0.0;
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
		}

		public Interval<SimulationResult> GetResult (double confidenceIntervalPercentage) {
			List<SimulationResult> resultsList = new List<SimulationResult> (results);
			resultsList.RemoveRange (0, l);
			return ConfidenceInterval.CreateInterval(resultsList, confidenceIntervalPercentage);
		}

		public override string ToString () {
			return string.Format("Batch Means ({0}, {1}, {2})", numBatches, batchLength, customersPerBatch);
		}
	}
}

