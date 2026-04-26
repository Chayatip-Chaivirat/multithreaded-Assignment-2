using System;
using System.Threading;

namespace multithreadedAssignment2
{
    internal class TransferAccount
    {
        public int Id { get; }
        public int Balance { get; private set; }

        public TransferAccount(int id, int balance)
        {
            Id = id;
            Balance = balance;
        }

        // DEADLOCK VERSION
        public void TransferDeadlock(TransferAccount toAccount, int amount)
        {
            lock (this) // Lock the current account
            {
                Console.WriteLine($"Account {Id} locked by Thread {Thread.CurrentThread.ManagedThreadId}");

                Thread.Sleep(500);

                lock (toAccount) // Lock the target account
                {
                    Console.WriteLine($"Account {toAccount.Id} locked by Thread {Thread.CurrentThread.ManagedThreadId}");

                    // Perform the transfer
                    Balance -= amount;
                    toAccount.Balance += amount;

                    Console.WriteLine($"{amount} transferred from {Id} to {toAccount.Id}");
                }
            }
        }

        // SAFE VERSION
        public void TransferSafe(TransferAccount toAccount, int amount)
        {
            TransferAccount first;
            TransferAccount second;

            if (Id < toAccount.Id) // Determine lock order based on account IDs 
            {
                first = this;
                second = toAccount;
            }
            else
            {
                first = toAccount;
                second = this;
            }

            lock (first)
            {
                lock (second)
                {
                    // Perform the transfer
                    Balance -= amount;
                    toAccount.Balance += amount;

                    Console.WriteLine($"{amount} transferred safely from {Id} to {toAccount.Id}");
                }
            }
        }
    }
}