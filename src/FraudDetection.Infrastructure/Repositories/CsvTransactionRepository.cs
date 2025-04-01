using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using FraudDetection.Domain.Entities;
using FraudDetection.Domain.Repositories;

namespace FraudDetection.Infrastructure.Repositories
{
  public class CsvTransactionRepository : ITransactionRepository
  {
    private readonly string _csvFilePath;

    public CsvTransactionRepository(string csvFilePath)
    {
      _csvFilePath = csvFilePath;
      Console.WriteLine($"CsvTransactionRepository using CSV file at: {_csvFilePath}");
    }

    public IEnumerable<Transaction> GetAllTransactions()
    {
      if (!File.Exists(_csvFilePath))
        throw new FileNotFoundException($"CSV file not found at {_csvFilePath}");

      using var reader = new StreamReader(_csvFilePath);
      using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
      return new List<Transaction>(csv.GetRecords<Transaction>());
    }

    public Transaction? GetById(string transactionID)
    {
      foreach (var tx in GetAllTransactions())
      {
        if (tx.TransactionID.Equals(transactionID, StringComparison.OrdinalIgnoreCase))
          return tx;
      }
      return null;
    }

    public IEnumerable<Transaction> StreamAllTransactions()
    {
      if (!File.Exists(_csvFilePath))
        throw new FileNotFoundException($"CSV file not found at {_csvFilePath}");

      var config = new CsvConfiguration(CultureInfo.InvariantCulture)
      {
        HasHeaderRecord = true,
        MissingFieldFound = null,
        BadDataFound = null,
        Delimiter = ","
      };

      using var reader = new StreamReader(_csvFilePath);
      using var csv = new CsvReader(reader, config);

      while (csv.Read())
      {
        Transaction record;
        try
        {
          record = csv.GetRecord<Transaction>();
          if (record.TransactionDate == default)
            continue;
        }
        catch
        {
          continue;
        }
        yield return record;
      }
    }
  }
}
