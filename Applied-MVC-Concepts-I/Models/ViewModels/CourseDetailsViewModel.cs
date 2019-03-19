using Applied_MVC_Concepts_I.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Applied_MVC_Concepts_I.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        public string Name { get; set; }
        public int NumberOfHours { get; set; }
        public int NumberOfEnrollments { get; set; }
        public List<string> AllEnrolledUsersNames { get; set; }

        public string GetAllEnrolledUsersNamesAsString { get => string.Join(", ", AllEnrolledUsersNames); }

        public static CourseDetailsViewModel GenerateNewViewModel(Course course)
        {
            return new CourseDetailsViewModel()
            {
                Name = course.Name,
                NumberOfEnrollments = course.NumberOfEnrollments,
                NumberOfHours = course.NumberOfHours,
                AllEnrolledUsersNames = course.Users.Select(user => user.FullName).ToList(),
            };
        }
    }
}