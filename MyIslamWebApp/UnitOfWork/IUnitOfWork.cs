using MyIslamWebApp.Repository.CustomDuas;
using MyIslamWebApp.Repository.DailyQuotes;
using MyIslamWebApp.Repository.DonationCategorys;
using MyIslamWebApp.Repository.Donations;
using MyIslamWebApp.Repository.DuaCategorys;
using MyIslamWebApp.Repository.Duas;
using MyIslamWebApp.Repository.Hadiths;
using MyIslamWebApp.Repository.HajjGuideCompletes;
using MyIslamWebApp.Repository.HajjGuides;
using MyIslamWebApp.Repository.HajjTaskCompletes;
using MyIslamWebApp.Repository.HajjTasks;
using MyIslamWebApp.Repository.InAppPurchases;
using MyIslamWebApp.Repository.Instructions;
using MyIslamWebApp.Repository.JumaQuotes;
using MyIslamWebApp.Repository.MakeDuas;
using MyIslamWebApp.Repository.MyEvents;
using MyIslamWebApp.Repository.PrayerRequests;
using MyIslamWebApp.Repository.QuranTranslates;
using MyIslamWebApp.Repository.Subscriptions;
using MyIslamWebApp.Repository.UmrahGuideCompletes;
using MyIslamWebApp.Repository.UmrahGuides;
using MyIslamWebApp.Repository.UmrahTaskCompletes;
using MyIslamWebApp.Repository.UmrahTasks;
using MyIslamWebApp.Repository.VotingOptions;
using MyIslamWebApp.Repository.Votings;

namespace MyIslamWebApp.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDailyQuoteRepository dailyQuoteRepository { get; }
        IJumaQuoteRepository jumaQuoteRepository { get; }
        IDuaRepository duaRepository { get; }
        IMyEventRepository myEventRepository { get; }
        IInstructionRepository instructionRepository { get; }
        IPrayerRequestRepository prayerRequestRepository { get; }
        IMakeDuaRepository MakeDuaRepository { get; }
        ICustomDuaRepository customDuaRepository { get; }
        IHadithRepository hadithRepository { get; }
        IDuaCategoryRepository duaCategoryRepository { get; }
        IDonationCategoryRepository donationCategoryRepository { get; }
        IVotingRepository votingRepository { get; }
        IVotingOptionRepository votingOptionRepository { get; }
        IHajjTaskRepository hajjTaskRepository { get; }
        IHajjTaskCompleteRepository hajjTaskCompleteRepository { get; }
        IUmrahTaskRepository umrahTaskRepository { get; }
        IUmrahTaskCompleteRepository umrahTaskCompleteRepository { get; }
        IHajjGuideRepository hajjGuideRepository { get; }
        IHajjGuideCompleteRepository hajjGuideCompleteRepository { get; }
        IUmrahGuideRepository umrahGuideRepository { get; }
        IUmrahGuideCompleteRepository umrahGuideCompleteRepository { get; }       
        IInAppPurchaseRepository inAppPurchaseRepository { get; }
        IDonationRepository donationRepository { get; }
        ISubscriptionRepository subscriptionRepository { get; }
        IQuranTranslateRepository quranTranslateRepository { get; }
        void Commit();
    }
}
