using System.Configuration;
using System.Data.Entity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using MyIslamWebApp.Entities;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.DataContext
{
    /// <summary>
    /// This class is the starting entry of our project with Entity Framework; 
    /// here we define connection string, add tables and other configurations
    /// </summary>
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        #region Properties
        //This property is used to loosely couple the connection string from this class
        //to a configuration file. Instead of passing the connection string into the constructor,
        //we'll read it from the configuration file and pass it here.
        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings["ConnectionStringName"] != null)
                    return ConfigurationManager.AppSettings["ConnectionStringName"].ToString();
                return "AuthContext";
            }
        }

        #endregion

        #region Constructor
        public AuthContext() : base(nameOrConnectionString: ConnectionStringName)
        {

            //Database.SetInitializer(new MyIslamAppDatabaseInitializer());
        }

        //Add models here to reflect as tables in database

        public DbSet<DailyQuote> DailyQuotes { get; set; }
        public DbSet<Dua> Duas { get; set; }
        public DbSet<MyEvent> MyEvents { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<JumaQuote> JumaQuotes { get; set; }
        public DbSet<PrayerRequest> PrayerRequests { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<MakeDua> MakeDuas { get; set; }
        public DbSet<CustomDua> CustomDuas { get; set; }
        public DbSet<Hadith> Hadiths { get; set; }
        public DbSet<DuaCategory> DuaCategorys { get; set; }
        public DbSet<DonationCategory> DonationCategorys { get; set; }
        public DbSet<Voting> Votings { get; set; }
        public DbSet<VotingOption> VotingOptions { get; set; }
        public DbSet<HajjTask> HajjTasks { get; set; }
        public DbSet<HajjTaskComplete> HajjTaskCompletes { get; set; }        
        public DbSet<UmrahTask> UmrahTasks { get; set; }
        public DbSet<UmrahTaskComplete> UmrahTaskCompletes { get; set; }
        public DbSet<HajjGuide> HajjGuides { get; set; }
        public DbSet<UmrahGuide> UmrahGuides { get; set; }
        public DbSet<HajjGuideComplete> HajjGuideCompletes { get; set; }
        public DbSet<UmrahGuideComplete> UmrahGuideCompletes { get; set; }      
        public DbSet<InAppPurchase> InAppPurchases { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<QuranTranslate> QuranTranslates { get; set; }

        //To seed data using the Database Initializer class
        //static AuthContext()
        //{
        //    Database.SetInitializer(new MyIslamAppDatabaseInitializer());
        //}

        #endregion

        //#region Methods
        ///// <summary>
        ///// This method is used to define custom settings on Models defined above
        ///// A common scenario is if in Model-First approach , suppose the tables are already created in database
        ///// and you want to map your models with it, simply use "To Table" 
        ///// Another scenario is by default, Entity Framework pluralize tables. 
        ///// To stop it from doing that remove the convention 'PluralizingEntitySetNameConvention' 
        ///// </summary>
        ///// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //This statement is used to register model configurations using FluentAPI for data annotations
        //    //modelBuilder.Configurations.Add(new EmployeeConfiguration());

        //    //This statement is used in case you have already created a table in database and wants to map your Model with it.
        //    //modelBuilder.Entity<Employee>().ToTable("tbl_Employee");
        //    //modelBuilder.Entity<Address>().ToTable("tbl_Address");
        //    //modelBuilder.Entity<Contact>().ToTable("tbl_Contact");
        //    //modelBuilder.Entity<Payroll>().ToTable("tbl_Payroll");

        //    //This statement will restrict Entity Framework to pluralize table names by default.
        //    modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        //}
        //#endregion
    }
}