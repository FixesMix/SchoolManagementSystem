namespace SchoolManagementSystem.Models
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; } //T can be any datatype
    }
}
