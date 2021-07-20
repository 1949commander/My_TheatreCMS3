using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Prod.Models
{
    public class CastDirector : ApplicationUser
    {
        public int HiredCastMembers { get; set; }
        public int FiredCastMembers { get; set; }
        ////adding additional properties to Cast Director
        //public string Name { get; set; }
        //public int DirectorId { get; set; }

        public static void CastDirectorSeed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //create user role
            if (!roleManager.RoleExists("DirectorManager"))
            {
                var role = new IdentityRole();
                role.Name = "DirectorManager";
                roleManager.Create(role);


                //create instance 
                var directorManager = new CastDirector()
                {
                    UserName = "castDirector",
                    Email = "CastDirector1@theatrevertigo.com",
                    HiredCastMembers = 30,
                    FiredCastMembers = 8
                };

                //creates user w/ password
                string castDirectorPwd = "castDirector01";

                var checkDirector = userManager.Create(directorManager, castDirectorPwd);

                if (checkDirector.Succeeded)
                {
                    userManager.AddToRole(directorManager.Id, "DirectorManager");
                }


            }

        }

    }
}