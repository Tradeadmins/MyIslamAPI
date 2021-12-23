using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyIslamWebApp.Entities;
using MyIslamWebApp.Enums;

namespace MyIslamWebApp.DataContext
{

    /// <summary>
    /// This class is used to seed the initial data into the database
    /// </summary>
    public class MyIslamAppDatabaseInitializer :
        //CreateDatabaseIfNotExists<AuthContext>
        DropCreateDatabaseAlways<AuthContext>
    {
        #region Seed Method
        /// <summary>
        /// This method is used to seed data into the tables
        /// </summary>
        /// <param name="context">Database Context of the project</param>
        protected override void Seed(AuthContext context)
        {
            if (context.Clients.Count() > 0)
            {
                return;
            }

            context.Clients.AddRange(BuildClientsList());
            context.SaveChanges();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AuthContext()));

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AuthContext()));

            ApplicationUser user = new ApplicationUser
            {
                UserName = "SuperUser",
                Email = "superuser@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PhoneNumber = "999999999",
                FirstName = "Super",
                LastName = "User",
                //FajrNotification = true,
                //DhudrNotification = true,
                //AsarNotification = true,
                //MagribNotification = true,
                //IshaNotification = true,
                //FajrAlarm = true,
                //DhudrAlarm = true,
                //AsarAlarm = true,
                //MagribAlarm = true,
                //IshaAlarm = true,
                //SubscriptionType = SubscriptionTypes.Yearly,
                //SubscriptionEndDate = DateTime.Now.AddDays(365)
            };

            _userManager.Create(user, "MySuperP@ss!");

            // Add Admin Role to use
            var adminUser = _userManager.FindByName("SuperUser");

            _userManager.AddToRole(adminUser.Id, "Admin");

        }
        #endregion

        #region Initial Data

        /// <summary>
        /// Initial Data for the Client table
        /// </summary>
        private List<Client> BuildClientsList()
        {
            List<Client> ClientsList = new List<Client>
            {
                new Client
                { Id = "ngAuthApp",
                    Secret= Helper.GetHash("abc@123"),
                    Name="AngularJS front-end Application",
                    ApplicationType =  ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "http://103.73.190.66:8080"
                },
                new Client
                { Id = "consoleApp",
                    Secret=Helper.GetHash("123@abc"),
                    Name="Console Application",
                    ApplicationType =ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
        #endregion
    }

}