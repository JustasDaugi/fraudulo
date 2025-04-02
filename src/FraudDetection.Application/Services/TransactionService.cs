using FraudDetection.Domain.Entities;
using FraudDetection.Domain.Repositories;
using FraudDetection.Application.Utilities;

namespace FraudDetection.Application.Services
{
  public interface ITransactionService
  {
    IEnumerable<Transaction> FetchTransactions();
    IEnumerable<Transaction> FetchFraudulentTransactions();
  }

  public class TransactionService : ITransactionService
  {
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
      _transactionRepository = transactionRepository;
    }

    public IEnumerable<Transaction> FetchTransactions()
    {
      return _transactionRepository.StreamAllTransactions();
    }

    public IEnumerable<Transaction> FetchFraudulentTransactions()
    {
      var transactions = _transactionRepository.StreamAllTransactions().ToList();
      return EvaluateFraud.FilterFraudulentTransactions(transactions);
    }
  }
}
