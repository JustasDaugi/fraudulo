using Microsoft.Extensions.DependencyInjection;
using FraudDetection.Domain.Repositories;
using FraudDetection.Infrastructure.Repositories;
using FraudDetection.Application.Utilities;
using FraudDetection.Application.Services;

namespace FraudDetection.Console
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var services = new ServiceCollection();
      const string csvPath = "csv-data/bank_transactions_data_2.csv";
      services.AddTransient<ITransactionRepository>(_ => new CsvTransactionRepository(csvPath));
      services.AddTransient<ITransactionService, TransactionService>();

      using var provider = services.BuildServiceProvider();
      var repository = provider.GetRequiredService<ITransactionRepository>();

      var streamedTransactions = repository.StreamAllTransactions().ToList();

      System.Console.WriteLine("-- Streaming Suspicious Transactions --");
      int count = 0;
      foreach (var tx in streamedTransactions)
      {
        var accountTxs = streamedTransactions
                          .Where(t => t.AccountID == tx.AccountID)
                          .OrderBy(t => t.TransactionDate)
                          .ToList();

        var triggeredRules = EvaluateFraud.Evaluate(tx, accountTxs);
        if (triggeredRules.Any())
        {
          System.Console.WriteLine($"TransactionID: {tx.TransactionID}");
          System.Console.WriteLine($"  TransactionAmount: {tx.TransactionAmount}");
          System.Console.WriteLine($"  AccountBalance: {tx.AccountBalance}");
          System.Console.WriteLine($"  TransactionDuration: {tx.TransactionDuration}");
          System.Console.WriteLine($"  LoginAttempts: {tx.CustomerProfile.LoginAttempts}");
          System.Console.WriteLine($"  Device IPAddress: {tx.Device.IPAddress}");
          foreach (var rule in triggeredRules)
          {
            System.Console.WriteLine("  -> " + rule);
          }
          System.Console.WriteLine("------------------------------------------------------");
          count++;
        }
      }
      System.Console.WriteLine($"\nTotal suspicious transactions: {count}");
    }
  }
}
