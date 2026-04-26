using System;
using System.Collections.Generic;
using System.Threading;

namespace multithreadedAssignment2
{
    internal class TransactionSimulator
    {
        private const int numberOfClients = 10;
        private const int runTime = 5000;
        private const double startBalance = 1000;

        public void RunRaceConditionSimulation()
        {
            Console.WriteLine("=== Race Condition Simulation ===");
            RunSimulation(false);
        }

        public void RunLockingSimulation()
        {
            Console.WriteLine("=== Locking Simulation ===");
            RunSimulation(true);
        }

        private void RunSimulation(bool useLocking)
        {
            BankAccount account = new BankAccount(startBalance);

            List<Client> clients = new List<Client>();
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < numberOfClients; i++)
            {
                Client client = new Client(i + 1, account);
                clients.Add(client);

                Thread thread;

                if (useLocking)
                    thread = new Thread(client.RunLocking);
                else
                    thread = new Thread(client.RunRaceCondition);

                threads.Add(thread);
            }

            foreach (Thread t in threads)
                t.Start();

            Thread.Sleep(runTime);

            foreach (Client c in clients)
                c.Stop();

            foreach (Thread t in threads)
                t.Join();

            double totalTransactions = 0;

            foreach (Client c in clients)
                totalTransactions += c.GetTotalTransactions();

            double expectedBalance = startBalance + totalTransactions;
            double actualBalance = account.GetBalance();
            double difference = expectedBalance - actualBalance;

            Console.WriteLine("Total Transactions: " + totalTransactions);
            Console.WriteLine("Expected Balance: " + expectedBalance);
            Console.WriteLine("Actual Balance:   " + actualBalance);
            Console.WriteLine("Difference:       " + difference);

            if (Math.Abs(difference) < 0.01)
                Console.WriteLine("SUCCESS: No race condition detected.");
            else
                Console.WriteLine("FAILURE: Race condition detected.");

            Console.WriteLine();
        }
    }
}