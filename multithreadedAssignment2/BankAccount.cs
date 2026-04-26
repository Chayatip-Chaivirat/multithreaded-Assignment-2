internal class BankAccount
{
    private double balance;
    private object lockObj = new object();

    public BankAccount(double start)
    {
        balance = start;
    }

    public void DepositRace(double amount)
    {
        double temp = balance; // Read current balance
        Thread.Sleep(1);    // Simulate some delay to increase chance
        balance = temp + amount; // Update balance with new amount
    }

    public void WithdrawRace(double amount)
    {
        double temp = balance; // Read current balance
        Thread.Sleep(1); // Simulate some delay to increase chance
        balance = temp - amount; // Update balance with new amount
    }

    public void DepositSafe(double amount)
    {
        lock (lockObj)
        {
            balance += amount;
        }
    }

    public void WithdrawSafe(double amount)
    {
        lock (lockObj)
        {
            balance -= amount;
        }
    }

    public double GetBalance()
    {
        return balance;
    }
}