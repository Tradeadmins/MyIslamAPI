namespace MyIslamWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shiv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomDuas",
                c => new
                    {
                        CustomDuaId = c.Int(nullable: false, identity: true),
                        CustomDuaName = c.String(nullable: false),
                        CustomDuaText = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomDuaId);
            
            CreateTable(
                "dbo.DailyQuotes",
                c => new
                    {
                        DailyQuoteId = c.Int(nullable: false, identity: true),
                        DailyQuoteText = c.String(nullable: false),
                        DailyQuoteLanguage = c.Int(nullable: false),
                        DailyQuoteValidOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DailyQuoteId);
            
            CreateTable(
                "dbo.DonationCategories",
                c => new
                    {
                        DonationCategoryId = c.Int(nullable: false, identity: true),
                        DonationCategoryName = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DonationCategoryId);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationId = c.Int(nullable: false, identity: true),
                        DonationCategoryId = c.Int(nullable: false),
                        DonationAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DonationLocalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DonationLocalCurrencyType = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DonationId)
                .ForeignKey("dbo.DonationCategories", t => t.DonationCategoryId, cascadeDelete: true)
                .Index(t => t.DonationCategoryId);
            
            CreateTable(
                "dbo.DuaCategories",
                c => new
                    {
                        DuaCategoryId = c.Int(nullable: false, identity: true),
                        DuaCategoryName = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DuaCategoryId);
            
            CreateTable(
                "dbo.Duas",
                c => new
                    {
                        DuaId = c.Int(nullable: false, identity: true),
                        DuaCategoryId = c.Int(nullable: false),
                        DuaName = c.String(nullable: false),
                        DuaArabicText = c.String(nullable: false),
                        DuaEnglishText = c.String(nullable: false),
                        DuaTurkeyText = c.String(nullable: false),
                        DuaMalayText = c.String(nullable: false),
                        DuaPronunciationText = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DuaId)
                .ForeignKey("dbo.DuaCategories", t => t.DuaCategoryId, cascadeDelete: true)
                .Index(t => t.DuaCategoryId);
            
            CreateTable(
                "dbo.Hadiths",
                c => new
                    {
                        HadithId = c.Int(nullable: false, identity: true),
                        HadithArabicText = c.String(nullable: false),
                        HadithEnglishText = c.String(nullable: false),
                        HadithTurkeyText = c.String(nullable: false),
                        HadithMalayText = c.String(nullable: false),
                        HadithPronunciationText = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HadithId);
            
            CreateTable(
                "dbo.HajjGuideCompletes",
                c => new
                    {
                        HajjGuideCompleteId = c.Int(nullable: false, identity: true),
                        HajjGuideId = c.Int(nullable: false),
                        HajjGuideCompleteByUserId = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HajjGuideCompleteId);
            
            CreateTable(
                "dbo.HajjGuides",
                c => new
                    {
                        HajjGuideId = c.Int(nullable: false, identity: true),
                        HajjGuideName = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HajjGuideId);
            
            CreateTable(
                "dbo.HajjTaskCompletes",
                c => new
                    {
                        HajjTaskCompleteId = c.Int(nullable: false, identity: true),
                        HajjTaskId = c.Int(nullable: false),
                        HajjTaskCompleteByUserId = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HajjTaskCompleteId);
            
            CreateTable(
                "dbo.HajjTasks",
                c => new
                    {
                        HajjTaskId = c.Int(nullable: false, identity: true),
                        HajjTaskName = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.HajjTaskId);
            
            CreateTable(
                "dbo.InAppPurchases",
                c => new
                    {
                        InAppPurchaseId = c.Int(nullable: false, identity: true),
                        InAppPurchaseByUserId = c.String(nullable: false),
                        InAppPurchaseTotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InAppPurchaseOwnerAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InAppPurchaseUserAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InAppPurchaseId);
            
            CreateTable(
                "dbo.Instructions",
                c => new
                    {
                        InstructionId = c.Int(nullable: false, identity: true),
                        InstructionLanguage = c.Int(nullable: false),
                        InstructionTitle = c.String(nullable: false),
                        InstructionDescription = c.String(nullable: false),
                        InstructionImageURL = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InstructionId);
            
            CreateTable(
                "dbo.JumaQuotes",
                c => new
                    {
                        JumaQuoteId = c.Int(nullable: false, identity: true),
                        JumaQuoteText = c.String(nullable: false),
                        JumaQuoteLanguage = c.Int(nullable: false),
                        JumaQuoteValidOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JumaQuoteId);
            
            CreateTable(
                "dbo.LogEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CallSite = c.String(),
                        Date = c.String(),
                        Exception = c.String(),
                        Level = c.String(),
                        Logger = c.String(),
                        MachineName = c.String(),
                        Message = c.String(),
                        StackTrace = c.String(),
                        Thread = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MakeDuas",
                c => new
                    {
                        MakeDuaId = c.Int(nullable: false, identity: true),
                        MakeDuaPrayerRequestId = c.Int(nullable: false),
                        MakeDuaByUserId = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MakeDuaId);
            
            CreateTable(
                "dbo.MyEvents",
                c => new
                    {
                        MyEventId = c.Int(nullable: false, identity: true),
                        MyEventCategory = c.Int(nullable: false),
                        MyEventName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        MobileNumber = c.String(),
                        Description = c.String(),
                        MyEventLatitude = c.Double(nullable: false),
                        MyEventLongitude = c.Double(nullable: false),
                        MyEventMinor = c.Boolean(nullable: false),
                        MyEventStartDate = c.DateTime(nullable: false),
                        MyEventEndDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MyEventId);
            
            CreateTable(
                "dbo.PrayerRequests",
                c => new
                    {
                        PrayerRequestId = c.Int(nullable: false, identity: true),
                        PrayerRequestText = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PrayerRequestId);
            
            CreateTable(
                "dbo.QuranTranslates",
                c => new
                    {
                        QuranTranslateId = c.Int(nullable: false, identity: true),
                        QuranTranslateLanguage = c.String(nullable: false),
                        QuranTranslateBy = c.String(nullable: false),
                        QuranTranslateUrl = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuranTranslateId);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false, identity: true),
                        SubscriptionByUserId = c.String(nullable: false),
                        SubscriptionType = c.Int(nullable: false),
                        SubscriptionEndDate = c.DateTime(nullable: false),
                        SubscriptionComplete = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriptionId);
            
            CreateTable(
                "dbo.UmrahGuideCompletes",
                c => new
                    {
                        UmrahGuideCompleteId = c.Int(nullable: false, identity: true),
                        UmrahGuideId = c.Int(nullable: false),
                        UmrahGuideCompleteByUserId = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UmrahGuideCompleteId);
            
            CreateTable(
                "dbo.UmrahGuides",
                c => new
                    {
                        UmrahGuideId = c.Int(nullable: false, identity: true),
                        UmrahGuideName = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UmrahGuideId);
            
            CreateTable(
                "dbo.UmrahTaskCompletes",
                c => new
                    {
                        UmrahTaskCompleteId = c.Int(nullable: false, identity: true),
                        UmrahTaskId = c.Int(nullable: false),
                        UmrahTaskCompleteByUserId = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UmrahTaskCompleteId);
            
            CreateTable(
                "dbo.UmrahTasks",
                c => new
                    {
                        UmrahTaskId = c.Int(nullable: false, identity: true),
                        UmrahTaskName = c.String(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UmrahTaskId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        SubscriptionComplete = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VotingOptions",
                c => new
                    {
                        VotingOptionId = c.Int(nullable: false, identity: true),
                        VotingId = c.Int(nullable: false),
                        DonationCategoryId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VotingOptionId)
                .ForeignKey("dbo.DonationCategories", t => t.DonationCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Votings", t => t.VotingId, cascadeDelete: true)
                .Index(t => t.VotingId)
                .Index(t => t.DonationCategoryId);
            
            CreateTable(
                "dbo.Votings",
                c => new
                    {
                        VotingId = c.Int(nullable: false, identity: true),
                        VotingTitle = c.String(nullable: false),
                        VotingStartDate = c.DateTime(nullable: false),
                        VotingEndDate = c.DateTime(nullable: false),
                        VotingDescription = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VotingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingOptions", "VotingId", "dbo.Votings");
            DropForeignKey("dbo.VotingOptions", "DonationCategoryId", "dbo.DonationCategories");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Duas", "DuaCategoryId", "dbo.DuaCategories");
            DropForeignKey("dbo.Donations", "DonationCategoryId", "dbo.DonationCategories");
            DropIndex("dbo.VotingOptions", new[] { "DonationCategoryId" });
            DropIndex("dbo.VotingOptions", new[] { "VotingId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Duas", new[] { "DuaCategoryId" });
            DropIndex("dbo.Donations", new[] { "DonationCategoryId" });
            DropTable("dbo.Votings");
            DropTable("dbo.VotingOptions");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UmrahTasks");
            DropTable("dbo.UmrahTaskCompletes");
            DropTable("dbo.UmrahGuides");
            DropTable("dbo.UmrahGuideCompletes");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.QuranTranslates");
            DropTable("dbo.PrayerRequests");
            DropTable("dbo.MyEvents");
            DropTable("dbo.MakeDuas");
            DropTable("dbo.LogEntries");
            DropTable("dbo.JumaQuotes");
            DropTable("dbo.Instructions");
            DropTable("dbo.InAppPurchases");
            DropTable("dbo.HajjTasks");
            DropTable("dbo.HajjTaskCompletes");
            DropTable("dbo.HajjGuides");
            DropTable("dbo.HajjGuideCompletes");
            DropTable("dbo.Hadiths");
            DropTable("dbo.Duas");
            DropTable("dbo.DuaCategories");
            DropTable("dbo.Donations");
            DropTable("dbo.DonationCategories");
            DropTable("dbo.DailyQuotes");
            DropTable("dbo.CustomDuas");
            DropTable("dbo.Clients");
        }
    }
}
