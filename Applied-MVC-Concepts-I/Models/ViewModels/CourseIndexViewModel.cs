using Applied_MVC_Concepts_I.Models.Domain;
using System;
using System.Collections.Generic;

namespace Applied_MVC_Concepts_I.Models.ViewModels
{
    public class CourseIndexViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfHours { get; set; }
        public int NumberOfEnrollments { get; set; }
        public static CourseIndexViewModel GenerateNewCourseViewModel(Course course)
        {
            return new CourseIndexViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                NumberOfEnrollments = course.NumberOfEnrollments,
                NumberOfHours = course.NumberOfHours,
            };
        }
    }
}