using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.Models;
using System.Data.Entity;

namespace MyIslamWebApp.Repository.HajjTaskCompletes
{
    /// <summary>
    /// CRUD operations for HajjTaskComplete table using Generic Repository Pattern
    /// </summary>
    public class HajjTaskCompleteRepository : RepositoryBase<HajjTaskComplete>, IHajjTaskCompleteRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public HajjTaskCompleteRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public HajjTaskCompleteRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        /// <summary>
        /// To add a new record in HajjTaskComplete table
        /// </summary>
        /// <param name="hajjTaskComplete">Instance of HajjTaskComplete class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddHajjTaskComplete(HajjTaskComplete hajjTaskComplete)
        {
            try
            {
                if (hajjTaskComplete == null)
                    return false;
                _dbContext.HajjTaskCompletes.Add(hajjTaskComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the HajjTaskComplete table
        /// </summary>
        /// <param name="hajjTaskCompleteId">The instance of HajjTaskComplete class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteHajjTaskComplete(int hajjTaskCompleteId)
        {
            try
            {
                if (hajjTaskCompleteId == 0)
                    return false;
                var deleteHajjTaskComplete = _dbContext.HajjTaskCompletes.FirstOrDefault(emp => emp.HajjTaskCompleteId == hajjTaskCompleteId);
                _dbContext.HajjTaskCompletes.Remove(deleteHajjTaskComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using HajjTaskComplete ID from Dua table
        /// </summary>
        /// <param name="hajjTaskCompleteId">Primary Key of the row (HajjTaskComplete ID)</param>
        /// <returns>Returns a Dua row matching the passing ID</returns>
        public HajjTaskComplete GetHajjTaskCompleteById(int hajjTaskCompleteId)
        {
            try
            {
                if (Equals(hajjTaskCompleteId, 0))
                    return null;

                return _dbContext.HajjTaskCompletes.AsNoTracking().Where(x => x.HajjTaskCompleteId == hajjTaskCompleteId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from HajjTaskComplete table
        /// </summary>
        /// <returns>An IQuerable List of HajjTaskComplete</returns>
        public IEnumerable<HajjTaskComplete> GetAllHajjTaskComplete()
        {
            try
            {
                return _dbContext.HajjTaskCompletes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all records using HajjTaskComplete Username of the HajjTaskComplete table
        /// </summary>
        /// <param name="HajjTaskCompleteUserId">HajjTaskComplete Username</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public IEnumerable<HajjTaskComplete> GetAllHajjTaskCompleteByUser(string HajjTaskCompleteUserId)
        {
            try
            {
                var data = _dbContext.HajjTaskCompletes.Where(x => x.HajjTaskCompleteByUserId == HajjTaskCompleteUserId).ToList();
                if (Equals(HajjTaskCompleteUserId, null))
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