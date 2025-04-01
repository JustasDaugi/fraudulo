namespace FraudDetection.Domain.Entities
{
  public class CustomerProfile
  {
    public int CustomerAge { get; set; }
    public string CustomerOccupation { get; set; } = string.Empty;
    public int LoginAttempts { get; set; }
  }
}
