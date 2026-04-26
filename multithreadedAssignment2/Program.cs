namespace multithreadedAssignment2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Program program = new Program();

            while (true)
            {
                program.MainMenu();
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    program.RaceCondition();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    program.Deadlock();
                }
                else if (choice == "0")
                {
                    break;
                }
            }
        }

        public void MainMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Race Condition");
            Console.WriteLine("2. Deadlock");
        }

        public void RaceCondition() // Method to run the race condition simulation
        {
            TransactionSimulator sim = new TransactionSimulator();

            sim.RunRaceConditionSimulation(); // Simulate the race condition (no locking)
            sim.RunLockingSimulation(); // Simulate the locking version 
        }

        public void Deadlock() // Method to run the deadlock simulation
        {
            DeadlockSimulator sim = new DeadlockSimulator();

            sim.RunDeadlock(); // Simulate the deadlock scenario (where two threads lock resources in opposite order)
            sim.RunSafeVersion(); // Simulate the safe version that prevents deadlock by enforcing a consistent lock order
        }
    }
}
