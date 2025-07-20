using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Provider.Interfaces;

namespace SchoolManagementSystem.Provider.Services
{
    public class FeeService : IFeeService
    {
        private readonly AppDbContext _dbContext;

        public FeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FeesResponse> AssignStudentFee(FeesRequest studentFee)
        {
            var fetchedStudents = await _dbContext.Students
                .Where(s => s.StudentLevel == studentFee.StudentLevel).ToListAsync();

            foreach (var student in fetchedStudents)
            {
                var feeAmt = new Fees
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.UserId,
                    StudentLevel = student.StudentLevel,
                    FeeAmount = studentFee.FeeAmount,
                    AmountOwed = studentFee.FeeAmount,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                if (student.StudentType == "Part Time") 
                {
                    var courses = student.StudentCourses.Select(c => c.CourseId).ToList(); //grab all courses the student is enrolled in 

                    var totalCourseAmt = await _dbContext.CourseFees
                        .Where(c => courses.Contains(c.CourseId)).SumAsync(x => x.Cost); // look up each course's fee from the course fee table
                                                                                         // and add the total to students fee

                    feeAmt.FeeAmount += totalCourseAmt; //adding
                }

                await _dbContext.Fees.AddAsync(feeAmt);
                await _dbContext.SaveChangesAsync();
            }

            return new FeesResponse //returns a basic confirmation of what was just assigned
            {
                FeeAmount = studentFee.FeeAmount,
                StudentLevel = studentFee.StudentLevel
            };
        }

        public async Task<CourseFeeResponse?> AddCourseFee(string courseId, decimal cost)
        {
            var courseFee = new CourseFees
            {
                CourseId = courseId,
                Cost = cost,
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            var addedCourseFee = await _dbContext.CourseFees.AddAsync(courseFee);
            if (addedCourseFee == null)
                return null;

            await _dbContext.SaveChangesAsync();
            return new CourseFeeResponse
            {
                FeeAmount = cost,
                CourseId = courseId
            };
   
        }

        public async Task<FeesResponse> GetStudentFee(string studentId)
        {
            var student = await _dbContext.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefaultAsync(s => s.UserId == studentId);

            if (student == null)
                return null;

            var courseIds = student.StudentCourses.Select(c => c.CourseId).ToList();

            var totalCourseFee = await _dbContext.CourseFees
                .Where(cf => courseIds.Contains(cf.CourseId))
                .SumAsync(cf => cf.Cost);

            return new FeesResponse
            {
                FeeAmount = totalCourseFee,
                StudentLevel = student.StudentLevel
            };
        }

        public async Task<FeesResponse> UpdateStudentFee(UpdateFeeRequest updatedFees)
        {
            var fee = await _dbContext.Fees.FirstOrDefaultAsync(f => f.StudentId == updatedFees.StudentId);
            if (fee == null) throw new Exception("Fee record not found.");

            fee.FeeAmount = updatedFees.FeeAmount;
            fee.AmountPaid = updatedFees.AmountPaid;
            fee.AmountOwed = updatedFees.FeeAmount - updatedFees.AmountPaid;
            fee.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return new FeesResponse
            {
                FeeAmount = fee.FeeAmount,
                StudentLevel = fee.StudentLevel
            };
        }

    }
}
