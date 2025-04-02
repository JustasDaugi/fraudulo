using FraudDetection.Domain.Entities;

namespace FraudDetection.Application.Utilities
{
  public static class EvaluateFraud
  {
    private static readonly TimeSpan IpChangeThreshold = TimeSpan.FromMinutes(15);

    public static List<string> Evaluate(Transaction tx, List<Transaction> accountTransactions)
    {
      var reasons = new List<string>();

      bool highRelativeAmount = tx.AccountBalance > 0 && 
      (tx.TransactionAmount / tx.AccountBalance) > 0.5m;
      
      if (highRelativeAmount)
        reasons.Add($"TransactionAmount ({tx.TransactionAmount}) is over 50% of AccountBalance ({tx.AccountBalance})");

      bool excessiveLoginAttempts = tx.CustomerProfile.LoginAttempts > 3;
      if (excessiveLoginAttempts)
        reasons.Add($"LoginAttempts ({tx.CustomerProfile.LoginAttempts}) are greater than 3");

      var sortedTransactions = accountTransactions.OrderBy(t => t.TransactionDate).ToList();
      
      int txIndex = sortedTransactions.FindIndex(t => t.TransactionID == tx.TransactionID);
      if (txIndex > 0)
      {
        var previousTx = sortedTransactions[txIndex - 1];
        bool rapidIpChange = previousTx.Device.IPAddress != tx.Device.IPAddress &&
          (tx.TransactionDate - previousTx.TransactionDate) <= IpChangeThreshold;

        if (rapidIpChange)
          reasons.Add($"Rapid IP change from {previousTx.Device.IPAddress} to {tx.Device.IPAddress} within {IpChangeThreshold.TotalMinutes} minutes");
      }

      return reasons;
    }

    public static bool IsFraudulent(Transaction tx, List<Transaction> accountTransactions)
    {
      return Evaluate(tx, accountTransactions).Any();
    }
    public static IEnumerable<Transaction> FilterFraudulentTransactions(IEnumerable<Transaction> transactions)
    {
      var transactionList = transactions.ToList();
      var grouped = transactionList
        .GroupBy(tx => tx.AccountID)
        .ToDictionary(g => g.Key, g => g.OrderBy(t => t.TransactionDate).ToList());
      return transactionList.Where(tx => IsFraudulent(tx, grouped[tx.AccountID]));
    }
  }
}
