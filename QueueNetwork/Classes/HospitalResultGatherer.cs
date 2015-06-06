using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class HospitalResultGatherer : ResultGathererInterface {
		private List<SimulationResult> results = new List<SimulationResult>();
		private SimulationResult currentResult;
		private int numOCustomers, numRCustomers, numDays, numDoctors, numOOOperations, numOROperations, numRROperations;
		private double[] lastOKService, OKServiceTime;
		private bool [] OKOutOfService;

		public HospitalResultGatherer (int numDoctors) {
			this.numDoctors = numDoctors;
			Initialize ();
		}

		public void Initialize() {
			currentResult = new SimulationResult ();
			numOCustomers = 0;
			numRCustomers = 0;
			numDays = 0;
			numOOOperations = 0;
			numOROperations = 0;
			numRROperations = 0;
			OKServiceTime = new double[numDoctors];
			lastOKService = new double[numDoctors];
			OKOutOfService = new bool[numDoctors];
			for (int i = 0; i < numDoctors; i++) {
				OKServiceTime [i] = 0.0;
				lastOKService [i] = 0.0;
				OKOutOfService [i] = true;
			}
		}

		public void AddOverTime(double overTime) {
			currentResult.OverTime += overTime;
		}

		public void AddODelay(double delay) {
			currentResult.ODelayTime += delay;
		}

		public void AddRDelay(double delay) {
			currentResult.RDelayTime += delay;
		}

		public void AddOCustomer () {
			numOCustomers++;
		}

		public void AddRCustomer () {
			numRCustomers++;
		}

		public void AddOOOperation () {
			numOOOperations++;
		}

		public void AddOROperation () {
			numOROperations++;
		}

		public void AddRROperation () {
			numRROperations++;
		}

		public void AddServiceTime(double serviceTime, OperationType operationType) {
			currentResult.TotalServiceTime += serviceTime;
			if (operationType == OperationType.OOG) {
				currentResult.OServiceTime += serviceTime;
			} else {
				currentResult.RServiceTime += serviceTime;
			}
		}

		public void AddNextDayQueue() {
			currentResult.Redirections += 1; // Cast to double
		}

		public void OKInService (int OKIndex, double simTime) {
			if (OKOutOfService [OKIndex]) {
				lastOKService [OKIndex] = simTime;
				OKOutOfService [OKIndex] = false;
			}
		}

		public void OKIdle (int OKIndex, double simTime) {
			if (!OKOutOfService [OKIndex]) {
				OKServiceTime [OKIndex] += (simTime - lastOKService [OKIndex]);
				OKOutOfService [OKIndex] = true;
			}
		}

		public void FinishDay () {
			numDays++;
		}

		public void Gather(double simTime) {
			if (numOCustomers > 0) {
				currentResult.ODelayTime /= numOCustomers;
				currentResult.OServiceTime /= numOCustomers;
			}
			if (numRCustomers > 0) {
				currentResult.RDelayTime /= numRCustomers;
				currentResult.RServiceTime /= numRCustomers;
			}
			currentResult.TotalServiceTime /= (numOCustomers + numRCustomers);
			if (numOOOperations + numOROperations > 0) {
				currentResult.OOOperationPercentage = ((double) (numOOOperations)) / ((double) (numOOOperations + numOROperations));
			}
			currentResult.ORDistribution = ((double)numOCustomers) / ((double)(numOCustomers + numRCustomers));
			currentResult.OverTime /= numDoctors;
			currentResult.Redirections /= numDays;

			double totalServicePercentage = 0.0;
			for (int i = 0; i < numDoctors; i++) {
				totalServicePercentage += OKServiceTime [i] / simTime;
			}
			totalServicePercentage /= numDoctors;
			//Console.WriteLine (totalServicePercentage);

			results.Add (currentResult);
			//Console.WriteLine (currentResult);
			Initialize ();
		}

		public List<SimulationResult> GetResults() {
			return results;
		}

		public Interval<SimulationResult> GetConfidenceInterval(double confidenceIntervalPercentage) {
			return ConfidenceInterval.CreateInterval(GetResults(), confidenceIntervalPercentage);
		}
	}
}

