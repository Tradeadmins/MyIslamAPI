using System;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.DailyQuotes;
using MyIslamWebApp.Repository.Duas;
using MyIslamWebApp.Repository.JumaQuotes;
using MyIslamWebApp.Repository.MyEvents;
using MyIslamWebApp.Repository.PrayerRequests;
using MyIslamWebApp.Repository.MakeDuas;
using MyIslamWebApp.Repository.Instructions;
using MyIslamWebApp.Repository.CustomDuas;
using MyIslamWebApp.Repository.Hadiths;
using MyIslamWebApp.Repository.DuaCategorys;
using MyIslamWebApp.Repository.DonationCategorys;
using MyIslamWebApp.Repository.Votings;
using MyIslamWebApp.Repository.VotingOptions;
using MyIslamWebApp.Repository.HajjTasks;
using MyIslamWebApp.Repository.HajjTaskCompletes;
using MyIslamWebApp.Repository.UmrahTasks;
using MyIslamWebApp.Repository.UmrahTaskCompletes;
using MyIslamWebApp.Repository.HajjGuides;
using MyIslamWebApp.Repository.UmrahGuides;
using MyIslamWebApp.Repository.UmrahGuideCompletes;
using MyIslamWebApp.Repository.HajjGuideCompletes;
using MyIslamWebApp.Repository.InAppPurchases;
using MyIslamWebApp.Repository.Donations;
using MyIslamWebApp.Repository.Subscriptions;
using MyIslamWebApp.Repository.QuranTranslates;

namespace MyIslamWebApp.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Properties
        private bool _disposed = false;
        private AuthContext _dbContext;
        #endregion

        #region Constructor
        public UnitOfWork()
        {
            CreateDbContext();
        }
        #endregion

        #region Repositories

        public IDailyQuoteRepository dailyQuoteRepository { get { return new DailyQuoteRepository(_dbContext); } }
        public IJumaQuoteRepository jumaQuoteRepository { get { return new JumaQuoteRepository(_dbContext); } }
        public IDuaRepository duaRepository { get { return new DuaRepository(_dbContext); } }
        public IMyEventRepository myEventRepository { get { return new MyEventRepository(_dbContext); } }
        public IInstructionRepository instructionRepository { get { return new InstructionRepository(_dbContext); } }
        public IPrayerRequestRepository prayerRequestRepository { get { return new PrayerRequestRepository(_dbContext); } }
        public IMakeDuaRepository MakeDuaRepository { get { return new MakeDuaRepository(_dbContext); } }
        public ICustomDuaRepository customDuaRepository { get { return new CustomDuaRepository(_dbContext); } }
        public IHadithRepository hadithRepository { get { return new HadithRepository(_dbContext); } }
        public IDuaCategoryRepository duaCategoryRepository { get { return new DuaCategoryRepository(_dbContext); } }
        public IDonationCategoryRepository donationCategoryRepository { get { return new DonationCategoryRepository(_dbContext); } }
        public IVotingRepository votingRepository { get { return new VotingRepository(_dbContext); } }
        public IVotingOptionRepository votingOptionRepository { get { return new VotingOptionRepository(_dbContext); } }
        public IHajjTaskRepository hajjTaskRepository { get { return new HajjTaskRepository(_dbContext); } }
        public IHajjTaskCompleteRepository hajjTaskCompleteRepository { get { return new HajjTaskCompleteRepository(_dbContext); } }
        public IUmrahTaskRepository umrahTaskRepository { get { return new UmrahTaskRepository(_dbContext); } }
        public IUmrahTaskCompleteRepository umrahTaskCompleteRepository { get { return new UmrahTaskCompleteRepository(_dbContext); } }
        public IHajjGuideRepository hajjGuideRepository { get { return new HajjGuideRepository(_dbContext); } }
        public IHajjGuideCompleteRepository hajjGuideCompleteRepository { get { return new HajjGuideCompleteRepository(_dbContext); } }
        public IUmrahGuideRepository umrahGuideRepository { get { return new UmrahGuideRepository(_dbContext); } }
        public IUmrahGuideCompleteRepository umrahGuideCompleteRepository { get { return new UmrahGuideCompleteRepository(_dbContext); } }    
        public IInAppPurchaseRepository inAppPurchaseRepository { get { return new InAppPurchaseRepository(_dbContext); } }
        public IDonationRepository donationRepository { get { return new DonationRepository(_dbContext); } }
        public ISubscriptionRepository subscriptionRepository { get { return new SubscriptionRepository(_dbContext); } }
        public IQuranTranslateRepository quranTranslateRepository { get { return new QuranTranslateRepository(_dbContext); } }

        #endregion

        #region Methods
        protected void CreateDbContext()
        {
            _dbContext = new AuthContext();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        #endregion

        #region IDisposable Implementation to dispose open connections/unused reference type objects
        public void Dispose()
        {
            Dispose(true);
            //Prevent the GC from calling Object.Finalize on an
            //object that does not require it
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
        #endregion
    }
}