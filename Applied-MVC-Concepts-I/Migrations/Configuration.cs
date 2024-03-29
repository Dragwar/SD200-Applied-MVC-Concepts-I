namespace Applied_MVC_Concepts_I.Migrations
{
    using Applied_MVC_Concepts_I.Models;
    using Applied_MVC_Concepts_I.Models.Domain;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal sealed class Configuration : DbMigrationsConfiguration<Applied_MVC_Concepts_I.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Applied_MVC_Concepts_I.Models.ApplicationDbContext";
        }

        protected override void Seed(Applied_MVC_Concepts_I.Models.ApplicationDbContext context)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser johnDoe = CreateUser(context, roleManager, userManager, "John", "Doe", "johndoe@test.com", "johndoe@test.com");
            ApplicationUser janeDoe = CreateUser(context, roleManager, userManager, "Jane", "Doe", "janedoe@test.com", "janedoe@test.com");

            Course softwareDev = CreateCourse(330, "Software Developer");
            Course cyberDef = CreateCourse(340, "Cyber Defense");
            Course networkSecurityDiploma = CreateCourse(400, " Network Security Diploma");


            softwareDev.Users.Add(johnDoe);

            softwareDev.Users.Add(janeDoe);
            cyberDef.Users.Add(janeDoe);

            context.Courses.AddOrUpdate(p => p.Name, softwareDev);
            context.Courses.AddOrUpdate(p => p.Name, cyberDef);
            context.Courses.AddOrUpdate(p => p.Name, networkSecurityDiploma);
            context.SaveChanges();
        }

        private Course CreateCourse(int numberOfHours, string courseName) => (
            new Course() { Id = Guid.NewGuid(), Name = courseName, NumberOfHours = numberOfHours }
        );
        

        private ApplicationUser CreateUser(
            Applied_MVC_Concepts_I.Models.ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            string firstName,
            string lastName,
            string userName,
            string userEmail,
            string userPassword = "Password-1",
            string userRole = null
            )
        {
            /// <summary>
            ///    Adding role if it doesn't exist. 
            /// </summary>
            if (!string.IsNullOrEmpty(userRole) && !context.Roles.Any(role => role.Name == userRole))
            {
                IdentityRole newRole = new IdentityRole(userRole);
                roleManager.Create(newRole);
            }

            // Creating the newUser
            ApplicationUser newUser;


            /// <summary>
            ///    Adding the default user and checks to see if the default user
            ///    already exists on the database before adding one.
            /// </summary>
            if (!context.Users.Any(user => user.UserName == userName))
            {
                newUser = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = userName,
                    Email = userEmail
                };

                userManager.Create(newUser, userPassword);
            }
            else
            {
                /// <summary>
                ///     I'm using ".First()" and not ".FirstOrDefault()" because
                ///     the if statement above this will generate the user if
                ///     the user doesn't already exist in the database
                ///     (I'm 100% expecting this user to be in the database)
                /// </summary>
                newUser = context.Users.First(user => user.UserName == userName);
            }

            // Make sure the user is on the proper role
            if (!string.IsNullOrEmpty(userRole) && !userManager.IsInRole(newUser.Id, userRole))
            {
                userManager.AddToRole(newUser.Id, userRole);
            }

            return newUser;
        }
    }
}
