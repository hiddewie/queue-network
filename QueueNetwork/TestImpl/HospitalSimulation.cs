using System;
using System.Collections.Generic;
using System.Text;

namespace QueueNetwork {

	class HospitalSimulation {
		private IDistribution 
		arrivalDistribution, 
		OServiceDistribution, 
		ORServiceDistribution, 
		RServiceDistribution,
		splitDistribution;
		private ISimulationMethod simMethod;
		private HospitalResultGatherer resultGatherer;
		private ParameterBag parameters;

		private double simTime;
		private string simTimeVisual;
		private SimulationEvent nextEventType;
		private int nextEventIndex;
		private ServerStatus[] serverStatus, lastServerStatus;
		private double timeLastEvent;
		private int numRegenerations;

		private int numCustomersDelayed;

		private Dictionary<SimulationEvent, double[]> timeNextEvent;
		private Queue<double> OQueue, RQueue, nextDayOQueue, nextDayRQueue;
		private TimeOfDay timeOfDay;

		private int totalOK, eyeOK, restOK;
		private double OChance, acceptanceThreshold;

		public static void Main (string[] args) {
			Console.WriteLine ("Starting simulations");
			Console.WriteLine ();

			double 
			hour = Constants.HOUR,
			min = Constants.MINUTE;
			int 
			numCustomers = 1500,
			numReplicationsRequired = 50;

			List<ParameterBag> parameters = new List<ParameterBag> ();
			int[][] parametersN = {
				new int[] { 20, 0 },
				new int[] { 20, 1 },
				//new int[] { 20, 3 },
				new int[] { 20, 5 },
				//new int[] { 20, 7 },
				new int[] { 20, 10 },
				//new int[] { 20, 12 },
				new int[] { 20, 15 },
				new int[] { 20, 19 },
			};
			double[][] parametersLambda = {
				// Lo = Lr
				new double[] { 1.0 / (3.0 * hour / 10.0), 1.0 / (3.0 * hour / 10.0) }, 
				new double[]{ 1.0 / (2.0 * hour / 10.0), 1.0 / (2.0 * hour / 10.0) }, 
				// Lo > Lr
				new double[]{ 1.0 / (3.0 * hour / 10.0), 1.0 / (4.0 * hour / 10.0) }, 
				new double[]{ 1.0 / (3.0 * hour / 20.0), 1.0 / (4.0 * hour / 20.0) }, 
				// Lo < Lr
				new double[]{ 1.0 / (4.0 * hour / 10.0), 1.0 / (3.0 * hour / 10.0) }, 
				new double[]{ 1.0 / (4.0 * hour / 20.0), 1.0 / (3.0 * hour / 20.0) }, 

				/*
				// 1:5
				new double[] { 1.0 / (3.0 * hour / 2.0), 1.0 / (3.0 * hour / 10.0) },
				// 1:3
				new double[] { 1.0 / (3.0 * hour / (10.0 / 3.0)), 1.0 / (3.0 * hour / (10.0 / 1.0)) },
				// 1:2
				new double[] { 1.0 / (3.0 * hour / (10.0 / 2.0)), 1.0 / (3.0 * hour / (10.0 / 1.0)) },
				// 2:1
				new double[] { 1.0 / (3.0 * hour / (10.0 / 1.0)), 1.0 / (3.0 * hour / (10.0 / 2.0)) },
				// 3:1
				new double[] { 1.0 / (3.0 * hour / (10.0 / 1.0)), 1.0 / (3.0 * hour / (10.0 / 3.0)) },
				// 5:1
				new double[] { 1.0 / (3.0 * hour / (10.0 / 1.0)), 1.0 / (3.0 * hour / (10.0 / 5.0)) },
				*/
			};

			double[] parametersP = {
				1.0 / 3.0,
				0.5,
				0.75,
				0.9,
				0.99,
			};

			foreach (double[] paramLambda in parametersLambda) {
				foreach (int[] paramN in parametersN) {
					foreach (double p in parametersP) {
						parameters.Add (new ParameterBag (paramLambda[0], paramLambda[1], 58.0 * min, 60.0 * min, 70.0 * min, 2.0 * hour, 1.0 * hour, p, paramN[0], paramN[1]));
					}
				}
			}

			foreach (ParameterBag param in parameters) {
				List<SimulationResult> results1 = new HospitalSimulation (
					                                  new ExponentialDistribution (param.LambdaO + param.LambdaR, 47), 
					                                  param.LambdaO / (param.LambdaO + param.LambdaR),
					                                  new UniformDistribution (param.OMin, param.OMax, 48), 
					                                  new UniformDistribution (param.Nu - 5.0 * min, param.Nu + 5.0 * min, 49), 
					                                  LogNormalDistribution.FromMuSigma (param.Mu, param.Sigma, 50),
					                                  new UniformDistribution (0.0, 1.0, 51),
					                                  new ReplicationSimulationMethod (numCustomers, numReplicationsRequired),
					                                  new HospitalResultGatherer (param.N),
													  param
				                                  ).Start (true);
			}
			/*List<SimulationResult> results2 = new HospitalSimulation (
				new ExponentialDistribution(lambdaO + lambdaR, 47), 
				lambdaO / (lambdaO + lambdaR),
				new UniformDistribution(OMin, OMax, 47), 
				new UniformDistribution(nu - 5.0 * Constants.MINUTE, nu + 5.0 * Constants.MINUTE, 47), 
				LogNormalDistribution.FromMuSigma(mu, sigma, 47),
				new UniformDistribution (0.0, 1.0, 47),
				new ReplicationMethod(numCustomers, numReplicationsRequired),
				new HospitalResultGatherer(N),
				N,
				n,
				0.1
			).Start (false);

			Interval<SimulationResult> simulationResult = ConfidenceInterval.CreateInterval (results1, results2, 95.0);

			Console.WriteLine("Simulation result:");
			Console.WriteLine("  Average O delay:                  [{0}, {1}]", Constants.FormatTime(simulationResult.Start.ODelayTime), Constants.FormatTime(simulationResult.End.ODelayTime));
			Console.WriteLine("  Average R delay:                  [{0}, {1}]", Constants.FormatTime(simulationResult.Start.RDelayTime), Constants.FormatTime(simulationResult.End.RDelayTime));
			//Console.WriteLine("  Average service time:              {0}", HospitalSimulation.FormatTime(ES));
			Console.WriteLine("  Average number of redirections:   [{0}, {1}]", simulationResult.Start.Redirections, simulationResult.End.Redirections);
			Console.WriteLine("  Average overtime:                 [{0}, {1}]", Constants.FormatTime(simulationResult.Start.OverTime), Constants.FormatTime(simulationResult.End.OverTime));
			Console.WriteLine("  Oog-Oog operations:               [{0:##.000000}, {1:##.000000}]", simulationResult.Start.OOOperationPercentage, simulationResult.End.OOOperationPercentage);
			Console.WriteLine("  O/R Distribution:                 [{0:##.000}, {1:##.000}]", simulationResult.Start.ORDistribution, simulationResult.End.ORDistribution);
*/

			Console.WriteLine ("Simulations ended");
			Console.WriteLine ();
			Console.WriteLine ("Press any key to exit...");
			Console.ReadKey ();
		}	

		public HospitalSimulation(
			IDistribution arrivalDistribution, 
			double OChance, 
			IDistribution OServiceDistribution, 
			IDistribution ORServiceDistribution, 
			IDistribution RServiceDistribution, 
			IDistribution splitDistribution, 
			ISimulationMethod simMethod,
			HospitalResultGatherer resultGatherer,
			ParameterBag parameters
		) {

			this.arrivalDistribution = arrivalDistribution;
			this.OChance = OChance;
			this.OServiceDistribution = OServiceDistribution;
			this.ORServiceDistribution = ORServiceDistribution;
			this.RServiceDistribution = RServiceDistribution;
			this.splitDistribution = splitDistribution;

			this.simMethod = simMethod;
			this.resultGatherer = resultGatherer;

			this.totalOK = parameters.N;
			this.eyeOK = parameters.SmallN;
			this.restOK = totalOK - eyeOK;
			this.acceptanceThreshold = parameters.P;

			this.parameters = parameters;
		}

		public List<SimulationResult> Start(bool printResult) {
			try {
				Console.WriteLine ("Simulating with method {0}", simMethod);

				Simulate();

				List<SimulationResult> rawResults = resultGatherer.GetResults();
				if (printResult) {
					Interval<SimulationResult> simulationResult = resultGatherer.GetConfidenceInterval(95.0);
					if (simulationResult != null) {
						double ORDistribution = simulationResult.Start.ORDistribution + (simulationResult.End.ORDistribution - simulationResult.Start.ORDistribution) / 2;
						double ES1 = simulationResult.Start.TotalServiceTime + (simulationResult.End.TotalServiceTime - simulationResult.Start.TotalServiceTime) / 2;
						double oop = simulationResult.Start.OOOperationPercentage + (simulationResult.End.OOOperationPercentage - simulationResult.Start.OOOperationPercentage) / 2;

						double oDel = simulationResult.Start.ODelayTime +  (simulationResult.End.ODelayTime - simulationResult.Start.ODelayTime) / 2;
						double rDel = simulationResult.Start.RDelayTime +  (simulationResult.End.RDelayTime - simulationResult.Start.RDelayTime) / 2;

						double ES2 = (double) (parameters.LambdaO / (parameters.LambdaO + parameters.LambdaR)) * (oop * OServiceDistribution.Expectation() + (1.0 - oop) * ORServiceDistribution.Expectation()) + (parameters.LambdaR / (parameters.LambdaO + parameters.LambdaR)) * RServiceDistribution.Expectation();
						StringBuilder sb = new StringBuilder();

						sb.AppendFormat("Simulation result (N: {0}, n: {1}, alpha: {2}, L_o: {3}, L_r: {4}):\n", totalOK, eyeOK, acceptanceThreshold, parameters.LambdaO, parameters.LambdaR);
						sb.AppendFormat("  Average O delay:                  [{0}, {1}]\n", Constants.FormatTime(simulationResult.Start.ODelayTime), Constants.FormatTime(simulationResult.End.ODelayTime));
						sb.AppendFormat("  Average R delay:                  [{0}, {1}]\n", Constants.FormatTime(simulationResult.Start.RDelayTime), Constants.FormatTime(simulationResult.End.RDelayTime));
						sb.AppendFormat("  Average O service time:           [{0}, {1}]\n", Constants.FormatTime(simulationResult.Start.OServiceTime), Constants.FormatTime(simulationResult.End.OServiceTime));
						sb.AppendFormat("  Average R service time:           [{0}, {1}]\n", Constants.FormatTime(simulationResult.Start.RServiceTime), Constants.FormatTime(simulationResult.End.RServiceTime));
						sb.AppendFormat("  Average number of redirections:   [{0:#.0}, {1:#.0}]\n", simulationResult.Start.Redirections, simulationResult.End.Redirections);
						sb.AppendFormat("  Average overtime:                 [{0}, {1}]\n", Constants.FormatTime(simulationResult.Start.OverTime), Constants.FormatTime(simulationResult.End.OverTime));
						sb.AppendFormat("  Oog-Oog operations:               [{0:##.000}, {1:##.000}]\n", simulationResult.Start.OOOperationPercentage, simulationResult.End.OOOperationPercentage);
						sb.AppendFormat("  O/R Distribution:                 [{0:##.000}, {1:##.000}]\n", simulationResult.Start.ORDistribution, simulationResult.End.ORDistribution);

						sb.AppendFormat("  Average total service time:       {0}\n", Constants.FormatTime( oDel  * ORDistribution + (1.0 - ORDistribution) * rDel));    

						sb.AppendFormat("Validation l1/(l1+l2): {0:#.000} = {1:#.000}\n", OChance, ORDistribution);
						sb.AppendFormat("Validation E(S): {0:#.0000} = {1:#.0000}\n", ES1, ES2);

						sb.AppendFormat("\n");

						System.IO.File.AppendAllText(@"D:\Desktop\out.txt", sb.ToString());
					} else {
						Console.WriteLine ("Error, no simulation result...");
					}
					Console.WriteLine ();
				}
				return rawResults;
			} catch (NoEventException nee) {
				Console.WriteLine ("No event at simulation time: {0}", simTime);
				Console.WriteLine (nee.ToString());
			}
			return null;
		}

		/**
		 * Simulates the simulation
		 */
		private void Simulate () {
			long totalSimulatedCustomers = 0;
			double totalSimulatedTime = 0.0;

			while (simMethod.ContinueSimulation ()) {
				Initialize ();
				simMethod.RunStarted ();

				while (simMethod.ContinueRun (simTime, numCustomersDelayed, numRegenerations)) {
					ServerStatus[] tempServerStatus = (ServerStatus[]) serverStatus.Clone();

					Timing ();

					if (nextEventType == SimulationEvent.ARRIVAL) {
						Arrive ();
					} else if (nextEventType == SimulationEvent.DEPART) {
						Depart (nextEventIndex);
					} else if (nextEventType == SimulationEvent.FIXED_TIMING) {
						FixedTiming ();
					}

					lastServerStatus = tempServerStatus;
				}

				simMethod.RunFinished (simTime, numCustomersDelayed);

				resultGatherer.Gather (simTime);

				//Console.WriteLine ("O Queue: {0} ({1}), R Queue: {2} ({3})", OQueue.Count, nextDayOQueue.Count, RQueue.Count, nextDayRQueue.Count);

				totalSimulatedCustomers += numCustomersDelayed;
				totalSimulatedTime += simTime;
			}
			Console.WriteLine ("Simulation ended after {0} days, customers simulated: {1}", (int) (totalSimulatedTime / Constants.DAY), totalSimulatedCustomers);
		}
		/**
		 * Initializes the simulation
		 */
		private void Initialize() {
			nextEventType = SimulationEvent.ARRIVAL;
			nextEventIndex = 0;

			SetSimTime (0.0);
			serverStatus = new ServerStatus[totalOK];
			lastServerStatus = new ServerStatus[totalOK];
			for (int i = 0; i < totalOK; i++) {
				serverStatus[i] = ServerStatus.IDLE;
				lastServerStatus[i] = ServerStatus.BUSY;
			}
			timeLastEvent = 0.0;
			numRegenerations = 0;

			numCustomersDelayed = 0;

			OQueue = new Queue<double> ();
			RQueue = new Queue<double> ();
			nextDayOQueue = new Queue<double> ();
			nextDayRQueue = new Queue<double> ();

			timeNextEvent = new Dictionary<SimulationEvent, double[]> {
				{SimulationEvent.ARRIVAL, new double[] { Constants.INF }},
				{SimulationEvent.FIXED_TIMING, new double[] { simMethod.GetFixedTiming() }},
			};

			timeNextEvent [SimulationEvent.DEPART] = new double[totalOK];
			for (int i = 0; i < totalOK; i++) {
				timeNextEvent [SimulationEvent.DEPART] [i] = Constants.INF;
			}
		}
		/**
		 * Updates the timing of the simulation
		 */
		private void Timing() {
			double minTimeNextEvent = Constants.INF / 10; // Smaller than INF

			bool foundNextEvent = false;
			foreach (KeyValuePair<SimulationEvent, double[]> evs in timeNextEvent) {
				for (int i = 0; i < evs.Value.Length; i++) {
					if (evs.Value[i] < minTimeNextEvent) {
						minTimeNextEvent = evs.Value[i];
						nextEventType = evs.Key;
						nextEventIndex = i;
						foundNextEvent = true;
					}
				}
			}

			if (!foundNextEvent) {
				throw new NoEventException ();
			}

			SetSimTime (minTimeNextEvent);

			timeLastEvent = simTime;
		}
		/**
		 * Called when a new customer arrives in the simulation. Redirects the customer to a server or a queue.
		 */
		private void Arrive () {
			timeNextEvent[SimulationEvent.ARRIVAL][0] = simTime + arrivalDistribution.NextDouble ();
			OperationType operationType;
			if (splitDistribution.NextDouble () <= OChance) {
				operationType = OperationType.OOG;
				resultGatherer.AddOCustomer ();
			} else {
				operationType = OperationType.REST;
				resultGatherer.AddRCustomer ();
			}

			int availableIndex = FindAvailableOK (operationType);
			if (availableIndex == -1) { // No available OK found
				if (operationType == OperationType.OOG) {
					OQueue.Enqueue (simTime);
				} else {
					RQueue.Enqueue (simTime);
				}
			} else {
				bool continueOperation = ContinueOperation (operationType, IsEyeOK(availableIndex) ? OperationType.OOG : OperationType.REST);
				if (continueOperation) {
					SetService (simTime, operationType, availableIndex);
				} else {
					if (operationType == OperationType.OOG) {
						nextDayOQueue.Enqueue (simTime);
					} else {
						nextDayRQueue.Enqueue (simTime);
					}
					resultGatherer.AddNextDayQueue ();
				}
			}
		}
		/**
		 * Called when a customer departs from a server. Tries to find a new customer for the server.
		 */
		private void Depart (int OKIndex) {
			if (IsEyeOK (OKIndex)) {
				bool newCustomer = false;
				while (OQueue.Count > 0) {
					double arrivalTime = OQueue.Dequeue ();
					bool continueOperation = ContinueOperation (OperationType.OOG, OperationType.OOG);
					if (continueOperation) {
						SetService (arrivalTime, OperationType.OOG, OKIndex);
						newCustomer = true;
						break;
					} else {
						nextDayOQueue.Enqueue (arrivalTime);
						resultGatherer.AddNextDayQueue ();
					}
				}
				if (!newCustomer) {
					//Console.WriteLine ("{0} is now idle", OKIndex);
					SetIdle (OKIndex);
					resultGatherer.OKIdle (OKIndex, simTime);
				}
			} else {
				bool newCustomer = false;

				while (OQueue.Count > 0 || RQueue.Count > 0) {
					double OArrivalTime = Constants.INF, RArrivalTime = Constants.INF;
					if (OQueue.Count > 0) {
						OArrivalTime = OQueue.Peek ();
					}
					if (RQueue.Count > 0) {
						RArrivalTime = RQueue.Peek ();
					}

					double arrivalTime;

					if (OArrivalTime < RArrivalTime) {
						arrivalTime = OQueue.Dequeue ();
						bool continueOperation = ContinueOperation (OperationType.OOG, OperationType.REST);
						if (continueOperation) {
							SetService (arrivalTime, OperationType.OOG, OKIndex);
							newCustomer = true;
							break;
						} else {
							nextDayOQueue.Enqueue (OArrivalTime);
							resultGatherer.AddNextDayQueue ();
						}
					} else {
						arrivalTime = RQueue.Dequeue ();
						bool continueOperation = ContinueOperation (OperationType.REST, OperationType.REST);
						if (continueOperation) {
							SetService (arrivalTime, OperationType.REST, OKIndex);
							newCustomer = true;
							break;
						} else {
							nextDayRQueue.Enqueue (arrivalTime);
							resultGatherer.AddNextDayQueue ();
						}
					}
				}

				if (!newCustomer) {
					//Console.WriteLine ("{0} is now idle", OKIndex);
					SetIdle (OKIndex);
					resultGatherer.OKIdle (OKIndex, simTime);
				}
			}
		}

		/**
		 * Called when a fixed timing event occurs. This happens when a new part of a day is initialized. 
		 */
		private void FixedTiming() {
			if (timeOfDay == TimeOfDay.EVENING) {
				timeNextEvent [SimulationEvent.ARRIVAL] [0] = Constants.INF;

				while (OQueue.Count > 0) {
					nextDayOQueue.Enqueue (OQueue.Dequeue ());
				}
				while (RQueue.Count > 0) {
					nextDayRQueue.Enqueue (RQueue.Dequeue ());
				}

				int busyOServers = 0, busyRServers = 0;
				double overTime = 0.0;
				// Finish off all operations that started. 
				for (int i = 0; i < totalOK; i++) {
					if (serverStatus [i] == ServerStatus.BUSY) {
						// Doctor works more than 8 hours.
						overTime += timeNextEvent [SimulationEvent.DEPART] [i] - simTime;
						//Console.WriteLine ("Customer from time {0} finishes at time {1}", timeNextEvent [SimulationEvent.DEPART] [i], simTime);
						if (IsEyeOK (i)) {
							busyOServers++;
						} else {
							busyRServers++;
						}
						resultGatherer.OKIdle (i, timeNextEvent [SimulationEvent.DEPART] [i]);
					}
					SetIdle (i);

				}
				resultGatherer.AddOverTime (overTime);
				resultGatherer.FinishDay ();

				/*Console.WriteLine ("End,   Q: {0} + {1}, B: {2} + {3}, O: {4} minutes", 
					nextDayOQueue.Count, nextDayRQueue.Count, busyOServers, busyRServers, (int) (overTime / Constants.MINUTE));*/

			} else if (timeOfDay == TimeOfDay.NOON) {
				/*Console.WriteLine ("Start, Q: {0} + {1}", 
					nextDayOQueue.Count, nextDayRQueue.Count);*/

				timeNextEvent [SimulationEvent.ARRIVAL] [0] = simTime + arrivalDistribution.NextDouble ();

				while (nextDayOQueue.Count > 0) {
					OQueue.Enqueue (nextDayOQueue.Dequeue ());
				}
				while (nextDayRQueue.Count > 0) {
					RQueue.Enqueue (nextDayRQueue.Dequeue ());
				}

				int availableIndex, count = RQueue.Count;
				for (int i = 0; i < count; i++) {
					availableIndex = FindAvailableOK (OperationType.REST);
					if (availableIndex != -1) {
						SetService (RQueue.Dequeue (), OperationType.REST, availableIndex);
					} else {
						break;
					}
				}
				count = OQueue.Count;
				for (int i = 0; i < count; i++) {
					availableIndex = FindAvailableOK (OperationType.OOG);
					if (availableIndex != -1) {
						SetService (OQueue.Dequeue (), OperationType.OOG, availableIndex);
					} else {
						break;
					}
				}
			}

			timeNextEvent [SimulationEvent.FIXED_TIMING][0] = simTime + simMethod.GetFixedTiming ();
		}
		/**
		 * Updates the simulation time
		 */
		private void SetSimTime(double time) {
			simTime = time;
			simTimeVisual = Constants.FormatTime (time);

			double dayTime = (time % Constants.DAY) / Constants.HOUR;
			if (dayTime < 8.00) {
				timeOfDay = TimeOfDay.MORNING;
			} else if (dayTime < 16.00) {
				timeOfDay = TimeOfDay.NOON;
			} else {
				timeOfDay = TimeOfDay.EVENING;
			}
		}

		/**
		 * Initializes a new service
		 */
		private void SetService(double arrivalTime, OperationType operationType, int OKIndex) {
			numCustomersDelayed++;
			serverStatus[OKIndex] = ServerStatus.BUSY;

			double serviceTime = ServiceDistribution (operationType, IsEyeOK (OKIndex)).NextDouble ();
			resultGatherer.AddServiceTime (serviceTime, operationType);
			timeNextEvent [SimulationEvent.DEPART][OKIndex] = simTime + serviceTime;

			resultGatherer.OKInService (OKIndex, simTime);

			if (operationType == OperationType.OOG) {
				resultGatherer.AddODelay (simTime - arrivalTime);
				if (IsEyeOK (OKIndex)) {
					resultGatherer.AddOOOperation ();
				} else {
					resultGatherer.AddOROperation ();
				}
			} else {
				resultGatherer.AddRDelay (simTime - arrivalTime);
				resultGatherer.AddRROperation ();
			}
		}
		/**
		 * Sets a server to idle
		 */
		private void SetIdle(int OKIndex) {
			serverStatus[OKIndex] = ServerStatus.IDLE;
			timeNextEvent [SimulationEvent.DEPART][OKIndex] = Constants.INF;
		}
		/**
		 * Determines whether the OK is an OOG OK
		 */
		private bool IsEyeOK(int OKIndex) {
			return OKIndex < eyeOK;
		}
		/**
		 * Determines the distribution of the operation in the specified OK
		 */
		private IDistribution ServiceDistribution(OperationType operationType, bool eyeOK) {
			if (operationType == OperationType.OOG) {
				if (eyeOK) {
					return OServiceDistribution;
				} else {
					return ORServiceDistribution;
				}
			} else {
				return RServiceDistribution;
			}
		}
		/**
		 * Determines whether an operation should continue
		 */
		private bool ContinueOperation (OperationType operationType, OperationType serverType ) {
			double timeOfDay = (simTime % Constants.DAY) / Constants.HOUR;
			double timeIn8Hours = (simTime % Constants.WORK_DAY) / Constants.HOUR;
			double timeLeft = (8.0 - timeIn8Hours) * Constants.HOUR;

			IDistribution sd = ServiceDistribution (operationType, serverType == OperationType.OOG);
			double chanceContinue = sd.Cdf (timeLeft);
			//Console.WriteLine ("Chance Continue O: {0} S: {1} P: {2}", operationType, serverType, chanceContinue);
			return chanceContinue > acceptanceThreshold;
		}
		/**
		 * Finds the first available OK
		 */
		private int FindAvailableOK (OperationType operationType) {
			int availableIndex = -1;
			if (timeOfDay == TimeOfDay.NOON) {
				if (operationType == OperationType.OOG) {
					for (int i = 0; i < eyeOK; i++) {
						if (serverStatus [i] == ServerStatus.IDLE) {
							availableIndex = i;
							//Console.WriteLine ("Found empty for {0} at {1}", operationType, i);
							break;
						}
					}
				}
				if (availableIndex == -1) {
					for (int i = eyeOK; i < totalOK; i++) {
						if (serverStatus [i] == ServerStatus.IDLE) {
							availableIndex = i;
							//Console.WriteLine ("Found empty for {0} at {1}", operationType, i);
							break;
						}
					}
				}
			}
			if (availableIndex == -1) {
				//Console.WriteLine ("Found no empty for {0} ({1})", operationType, timeOfDay);
			}
			return availableIndex;
		}
	}
}
