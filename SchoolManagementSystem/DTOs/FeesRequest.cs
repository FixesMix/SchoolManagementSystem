namespace SchoolManagementSystem.DTOs
{
    public class FeesRequest
    {
        //what the user is sending
        public decimal FeeAmount { get; set; }
        public int StudentLevel { get; set; }
    }
}
