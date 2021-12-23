using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.Models;
using System.Data.Entity;

namespace MyIslamWebApp.Repository.UmrahGuideCompletes
{
    /// <summary>
    /// CRUD operations for UmrahGuideComplete table using Generic Repository Pattern
    /// </summary>
    public class UmrahGuideCompleteRepository : RepositoryBase<UmrahGuideComplete>, IUmrahGuideCompleteRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public UmrahGuideCompleteRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public UmrahGuideCompleteRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        /// <summary>
        /// To add a new record in UmrahGuideComplete table
        /// </summary>
        /// <param name="umrahGuideComplete">Instance of UmrahGuideComplete class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddUmrahGuideComplete(UmrahGuideComplete umrahGuideComplete)
        {
            try
            {
                if (umrahGuideComplete == null)
                    return false;
                _dbContext.UmrahGuideCompletes.Add(umrahGuideComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the UmrahGuideComplete table
        /// </summary>
        /// <param name="umrahGuideCompleteId">The instance of UmrahGuideComplete class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteUmrahGuideComplete(int umrahGuideCompleteId)
        {
            try
            {
                if (umrahGuideCompleteId == 0)
                    return false;
                var deleteUmrahGuideComplete = _dbContext.UmrahGuideCompletes.FirstOrDefault(emp => emp.UmrahGuideCompleteId == umrahGuideCompleteId);
                _dbContext.UmrahGuideCompletes.Remove(deleteUmrahGuideComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using UmrahGuideComplete ID from Dua table
        /// </summary>
        /// <param name="umrahGuideCompleteId">Primary Key of the row (UmrahGuideComplete ID)</param>
        /// <returns>Returns a Dua row matching the passing ID</returns>
        public UmrahGuideComplete GetUmrahGuideCompleteById(int umrahGuideCompleteId)
        {
            try
            {
                if (Equals(umrahGuideCompleteId, 0))
                    return null;

                return _dbContext.UmrahGuideCompletes.AsNoTracking().Where(x => x.UmrahGuideCompleteId == umrahGuideCompleteId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from UmrahGuideComplete table
        /// </summary>
        /// <returns>An IQuerable List of UmrahGuideComplete</returns>
        public IEnumerable<UmrahGuideComplete> GetAllUmrahGuideComplete()
        {
            try
            {
                return _dbContext.UmrahGuideCompletes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all records using UmrahGuideComplete Username of the UmrahGuideComplete table
        /// </summary>
        /// <param name="UmrahGuideCompleteUserId">UmrahGuideComplete Username</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public IEnumerable<UmrahGuideComplete> GetAllUmrahGuideCompleteByUser(string UmrahGuideCompleteUserId)
        {
            try
            {
                var data = _dbContext.UmrahGuideCompletes.Where(x => x.UmrahGuideCompleteByUserId == UmrahGuideCompleteUserId).ToList();
                if (Equals(UmrahGuideCompleteUserId, null))
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