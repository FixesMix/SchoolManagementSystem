﻿namespace SchoolManagementSystem.DTOs.Grades
{
    public class GradingRequest
    {
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public double Score { get; set; }

    }
}
