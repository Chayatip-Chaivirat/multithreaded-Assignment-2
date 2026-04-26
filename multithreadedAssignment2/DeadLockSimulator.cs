using System;
using System.Threading;

namespace multithreadedAssignment2
{
    internal class DeadlockSimulator
    {
        public void RunDeadlock()
        {
            Console.WriteLine("=== Deadlock Simulation ===");

            // Create two accounts with initial balances
            TransferAccount A = new TransferAccount(1, 1000);
            TransferAccount B = new TransferAccount(2, 1000);

            // Create two threads that will attempt to transfer money between the accounts
            Thread t1 = new Thread(() => A.TransferDeadlock(B, 100));
            Thread t2 = new Thread(() => B.TransferDeadlock(A, 200));

            // Start both threads
            t1.Start();
            t2.Start();

            // Wait for both threads to complete with a timeout to detect deadlock
            if (!t1.Join(3000) || !t2.Join(3000)) // Wait for 3 seconds and check if threads are still running
            {
                Console.WriteLine("DEADLOCK DETECTED!"); // If either thread is still running after the timeout, we assume a deadlock has occurred
            }

            Console.WriteLine();
        }

        public void RunSafeVersion()
        {
            Console.WriteLine("=== Deadlock Prevention ===");

            // Create two accounts with initial balances
            TransferAccount A = new TransferAccount(1, 1000);
            TransferAccount B = new TransferAccount(2, 1000);

            // Create two threads that will attempt to transfer money between the accounts
            Thread t1 = new Thread(() => A.TransferSafe(B, 100));
            Thread t2 = new Thread(() => B.TransferSafe(A, 200));

            // Start both threads
            t1.Start();
            t2.Start();

            // Wait for both threads to complete without timeout
            t1.Join();
            t2.Join();

            Console.WriteLine("Transfers completed successfully.");
            Console.WriteLine();
        }
    }
}