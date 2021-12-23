namespace MyIslamWebApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataContext;
    using Entities;
    using Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

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
                SubscriptionComplete = false,
                FirstName = "Super",
                LastName = "User",               
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
                {   Id = "consoleApp",
                    Secret=Helper.GetHash("123@abc"),
                    Name="Console Application",
                    ApplicationType =ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                },
                new Client
                {   Id = "ngAdminApp",
                    Secret=Helper.GetHash("123@abc"),
                    Name="Angular Admin App",
                    ApplicationType =ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "http://localhost:53942"
                }
            };

            return ClientsList;
        }
        #endregion
    }
}
