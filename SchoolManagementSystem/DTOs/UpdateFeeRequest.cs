namespace SchoolManagementSystem.DTOs
{
    public class UpdateFeeRequest
    {
        public string StudentId { get; set; }
        public int StudentLevel { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
