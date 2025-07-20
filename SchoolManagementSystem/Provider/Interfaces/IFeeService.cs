using SchoolManagementSystem.DTOs;

namespace SchoolManagementSystem.Provider.Interfaces
{
    public interface IFeeService
    {
        //background services (application thats always running)

        //update fee, add fee, get fees, 

        Task<FeesResponse> AssignStudentFee(FeesRequest studentFee);
        Task<CourseFeeResponse?> AddCourseFee(string courseId, decimal cost);
        Task<FeesResponse> GetStudentFee(string studentId);
        Task<FeesResponse> UpdateStudentFee(UpdateFeeRequest updatedFees);
    }
}
