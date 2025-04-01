namespace FraudDetection.Domain.Entities
{
  public class Transaction
  {
    public string TransactionID { get; set; } = string.Empty;
    public string AccountID { get; set; } = string.Empty;
    public decimal TransactionAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string MerchantID { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public decimal AccountBalance { get; set; }
    public DateTime PreviousTransactionDate { get; set; }
    public int TransactionDuration { get; set; }

    public CustomerProfile CustomerProfile { get; set; } = new CustomerProfile();
    public Device Device { get; set; } = new Device();
  }
}
