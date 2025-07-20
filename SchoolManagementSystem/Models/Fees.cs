namespace SchoolManagementSystem.Models
{
    public class Fees
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; } 
        public int StudentLevel {  get; set; }
        public decimal FeeAmount { get; set; }
        public decimal AmountOwed { get; set; } //feeamount - amountpaid
        public decimal AmountPaid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
