using System.Collections.Generic;
using FraudDetection.Domain.Entities;

namespace FraudDetection.Domain.Repositories
{
  public interface ITransactionRepository
  {
    IEnumerable<Transaction> GetAllTransactions();
    Transaction? GetById(string transactionID);
    IEnumerable<Transaction> StreamAllTransactions();
  }
}
