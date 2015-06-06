﻿using System;
using System.Collections.Generic;

using QueueNetwork.Simulation.Result;

namespace QueueNetwork.Simulation.SimulationMethod {
	public class ReplicationSimulationMethod : ISimulationMethod {
		private int numDelaysRequired, numReplicationsRequired;
		private int numReplications = 0;
		private List<SimulationResult> results = new List<SimulationResult> ();

		public ReplicationSimulationMethod(int numDelaysRequired, int numReplicationsRequired) {
			this.numDelaysRequired = numDelaysRequired;
			this.numReplicationsRequired = numReplicationsRequired;
		}

		public double GetFixedTiming () {
			return Constants.WORK_DAY;
		}

		public bool ContinueSimulation () {
			return numReplications < numReplicationsRequired;
		}

		public bool ContinueRun (double simTime, int numCustomersDelayed, int numRegenerations) {
			return numCustomersDelayed < numDelaysRequired;
		}

		public void ServiceStarted (double simTime, double arrivalTime, int serverIndex) {

		}

		public void ServiceEnded (double simTime, int serverIndex) {

		}
	
		public void RunStarted () {

		}

		public void RunFinished (double simTime, int numCustomersDelayed) {
			numReplications++;
		}

		public override string ToString () {
			return string.Format("Replication ({0} customers, {1} replications)", numDelaysRequired, numReplicationsRequired);
		}
	}
}
