namespace Applied_MVC_Concepts_I.Migrations
{
    using Applied_MVC_Concepts_I.Models;
    using Applied_MVC_Concepts_I.Models.Domain;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Applied_MVC_Concepts_I.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Applied_MVC_Concepts_I.Models.ApplicationDbContext";
        }

        protected override void Seed(Applied_MVC_Concepts_I.Models.ApplicationDbContext context)
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser johnDoe;
            ApplicationUser janeDoe;

            /*NOTE: failed to create a many-to-many*/

            if (!context.Users.Any(user => user.UserName == "johndoe@test.com"))
            {
                johnDoe = new ApplicationUser
                {
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "johndoe@test.com",
                    Email = "johndoe@test.com",
                };

                userManager.Create(johnDoe, "Password-1");
            }
            else
            {
                johnDoe = context.Users.First(user => user.UserName == "johndoe@test.com");
            }

            if (!context.Users.Any(user => user.UserName == "janeDoe@test.com"))
            {
                janeDoe = new ApplicationUser
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    UserName = "janeDoe@test.com",
                    Email = "janeDoe@test.com",
                };

                userManager.Create(janeDoe, "Password-1");
            }
            else
            {
                janeDoe = context.Users.First(user => user.UserName == "janeDoe@test.com");
            }

            Course SoftwareDev;
            Course CyberDef;

            if (!context.Courses.Any(course => course.Name == "Software Developer"))
            {
                SoftwareDev = new Course() { Id = Guid.NewGuid(), Name = "Software Developer", NumberOfHours = 330 };
                context.Courses.Add(SoftwareDev);
            }
            else
            {
                SoftwareDev = context.Courses.First(course => course.Name == "Software Developer");
            }

            if (!context.Courses.Any(course => course.Name == "Cyber Defense"))
            {
                CyberDef = new Course() { Id = Guid.NewGuid(), Name = "Cyber Defense", NumberOfHours = 340 };
                context.Courses.Add(CyberDef);
            }
            else
            {
                CyberDef = context.Courses.First(course => course.Name == "Cyber Defense");
            }


            if (!johnDoe.Courses.Any())
            {
                SoftwareDev.Users.Add(johnDoe);
                johnDoe.Courses.Add(SoftwareDev);
            }
            if (!janeDoe.Courses.Any())
            {
                SoftwareDev.Users.Add(janeDoe);
                CyberDef.Users.Add(janeDoe);
                janeDoe.Courses.Add(SoftwareDev);
                janeDoe.Courses.Add(CyberDef);
            }



            context.SaveChanges();
        }
    }
}
