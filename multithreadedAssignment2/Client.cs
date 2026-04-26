internal class Client
{
    private int clientId;
    private BankAccount account;
    private bool running = true;
    private double totalTransactions = 0;
    private Random random = Random.Shared;

    public Client(int id, BankAccount acc)
    {
        clientId = id;
        account = acc;
    }

    public void RunRaceCondition()
    {
        while (running)
        {
            int amount = random.Next(1, 100);

            if (random.Next(2) == 0)
            {
                account.DepositRace(amount);
                totalTransactions += amount;
            }
            else
            {
                account.WithdrawRace(amount);
                totalTransactions -= amount;
            }
        }
    }

    public void RunLocking()
    {
        while (running)
        {
            int amount = random.Next(1, 100);

            if (random.Next(2) == 0)
            {
                account.DepositSafe(amount);
                totalTransactions += amount;
            }
            else
            {
                account.WithdrawSafe(amount);
                totalTransactions -= amount;
            }
        }
    }

    public void Stop()
    {
        running = false;
    }

    public double GetTotalTransactions()
    {
        return totalTransactions;
    }
}