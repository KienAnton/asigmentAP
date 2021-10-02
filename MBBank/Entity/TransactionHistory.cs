using System;

namespace MBBank.Entity
{
    public class TransactionHistory
    {
        public string Id { get; set; }
    public string SenderAccountNumber { get; set; }
    public string ReceiverAccountNumber { get; set; }
    public int Withdraw { get; set; }
    public double Deposit { get; set; }
    public double Transfer { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public int Status { get; set; }
    
    }
}