using FraudDetection.Domain.Entities;

namespace FraudDetection.Domain.Repositories
{
  public interface ITransactionRepository
  {
    Transaction? GetById(string transactionID);
    IEnumerable<Transaction> StreamAllTransactions();
  }
}
