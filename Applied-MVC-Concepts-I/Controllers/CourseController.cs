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
        private Course GetCourseById(Guid id) => GetCourses().FirstOrDefault(course => course.Id == id);

        public ActionResult Index()
        {
            List<CourseIndexViewModel> model = GetCourses()
                .Select(course => CourseIndexViewModel.GenerateNewViewModel(course))
                .ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult CourseDetails(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction(nameof(CourseController.Index));

            Course foundCourse = GetCourseById(id.Value);

            if (foundCourse == null)
                return RedirectToAction(nameof(CourseController.Index));

            CourseDetailsViewModel model = CourseDetailsViewModel.GenerateNewViewModel(foundCourse);

            return View(model);
        }
    }
}