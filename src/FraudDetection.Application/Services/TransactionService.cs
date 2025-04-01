using System.Collections.Generic;
using FraudDetection.Domain.Entities;
using FraudDetection.Domain.Repositories;

namespace FraudDetection.Application.Services
{
  public interface ITransactionService
  {
    IEnumerable<Transaction> FetchTransactions();
    Transaction GetTransactionDetails(string transactionID);
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
      return _transactionRepository.GetAllTransactions();
    }

    public Transaction GetTransactionDetails(string transactionID)
    {
      return _transactionRepository.GetById(transactionID);
    }
  }
}
