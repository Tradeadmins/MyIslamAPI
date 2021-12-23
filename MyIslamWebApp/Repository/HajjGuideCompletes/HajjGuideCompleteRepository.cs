using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.Models;
using System.Data.Entity;

namespace MyIslamWebApp.Repository.HajjGuideCompletes
{
    /// <summary>
    /// CRUD operations for HajjGuideComplete table using Generic Repository Pattern
    /// </summary>
    public class HajjGuideCompleteRepository : RepositoryBase<HajjGuideComplete>, IHajjGuideCompleteRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public HajjGuideCompleteRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public HajjGuideCompleteRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        /// <summary>
        /// To add a new record in HajjGuideComplete table
        /// </summary>
        /// <param name="hajjGuideComplete">Instance of HajjGuideComplete class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddHajjGuideComplete(HajjGuideComplete hajjGuideComplete)
        {
            try
            {
                if (hajjGuideComplete == null)
                    return false;
                _dbContext.HajjGuideCompletes.Add(hajjGuideComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the HajjGuideComplete table
        /// </summary>
        /// <param name="hajjGuideCompleteId">The instance of HajjGuideComplete class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteHajjGuideComplete(int hajjGuideCompleteId)
        {
            try
            {
                if (hajjGuideCompleteId == 0)
                    return false;
                var deleteHajjGuideComplete = _dbContext.HajjGuideCompletes.FirstOrDefault(emp => emp.HajjGuideCompleteId == hajjGuideCompleteId);
                _dbContext.HajjGuideCompletes.Remove(deleteHajjGuideComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// To get a single record using HajjGuideComplete ID from Dua table
        /// </summary>
        /// <param name="hajjGuideCompleteId">Primary Key of the row (HajjGuideComplete ID)</param>
        /// <returns>Returns a Dua row matching the passing ID</returns>
        public HajjGuideComplete GetHajjGuideCompleteById(int hajjGuideCompleteId)
        {
            try
            {
                if (Equals(hajjGuideCompleteId, 0))
                    return null;

                return _dbContext.HajjGuideCompletes.AsNoTracking().Where(x => x.HajjGuideCompleteId == hajjGuideCompleteId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from HajjGuideComplete table
        /// </summary>
        /// <returns>An IQuerable List of HajjGuideComplete</returns>
        public IEnumerable<HajjGuideComplete> GetAllHajjGuideComplete()
        {
            try
            {
                return _dbContext.HajjGuideCompletes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all records using HajjGuideComplete Username of the HajjGuideComplete table
        /// </summary>
        /// <param name="HajjGuideCompleteUserId">HajjGuideComplete Username</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public IEnumerable<HajjGuideComplete> GetAllHajjGuideCompleteByUser(string HajjGuideCompleteUserId)
        {
            try
            {
                var data = _dbContext.HajjGuideCompletes.Where(x => x.HajjGuideCompleteByUserId == HajjGuideCompleteUserId).ToList();
                if (Equals(HajjGuideCompleteUserId, null))
                    return data;
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}