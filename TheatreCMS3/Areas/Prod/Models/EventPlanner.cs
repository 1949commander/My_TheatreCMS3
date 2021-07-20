using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Prod.Models
{
    public class EventPlanner : ApplicationUser
    {
        public DateTime PlannerStartDate { get; set; }

        public static void Seed(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (!roleManager.RoleExists("EventPlanner"))
            {
                var role = new IdentityRole()
                {
                    Name = "EventPlanner"
                };
                roleManager.Create(role);

                var eventPlanner = new EventPlanner()
                {
                    UserName = "EventPlanner",
                    Email = "plannerofevents@email.com",
                    PlannerStartDate = new DateTime(2021,1,1),
                };
                string password = "password123";
                var checkPlanner = userManager.Create(eventPlanner, password);

                if (checkPlanner.Succeeded)
                {
                    userManager.AddToRole(eventPlanner.Id, "EventPlanner");
                }
            }
        }
    }
}