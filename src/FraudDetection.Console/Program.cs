using System;
using Microsoft.Extensions.DependencyInjection;
using FraudDetection.Domain.Repositories;
using FraudDetection.Application.Services;
using FraudDetection.Infrastructure.Repositories;

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

      var transactionService = provider.GetRequiredService<ITransactionService>();
      foreach (var tx in transactionService.FetchTransactions())
    //  Print additional columns
      {
        System.Console.WriteLine($"{tx.TransactionID} => {tx.TransactionAmount}");
      }

      System.Console.WriteLine("\n-- Streaming Transactions --");
      var repo = (CsvTransactionRepository)provider.GetRequiredService<ITransactionRepository>();
      foreach (var streamedTx in repo.StreamAllTransactions())
      {
        System.Console.WriteLine($"{streamedTx.TransactionID} => {streamedTx.TransactionAmount}");
      }
    }
  }
}
