using Applied_MVC_Concepts_I.Models;
using Applied_MVC_Concepts_I.Models.Domain;
using Applied_MVC_Concepts_I.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Applied_MVC_Concepts_I.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext DbContext;

        public CourseController()
        {
            DbContext = new ApplicationDbContext();
        }

        private List<Course> GetCourses() => DbContext.Courses.ToList();

        public ActionResult Index()
        {
            List<CourseIndexViewModel> model = GetCourses()
                .Select(course => CourseIndexViewModel.GenerateNewCourseViewModel(course))
                .ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult CourseDetails(Guid? id)
        {

            return View();
        }
    }
}