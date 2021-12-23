using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.Models;
using System.Data.Entity;

namespace MyIslamWebApp.Repository.UmrahTaskCompletes
{
    /// <summary>
    /// CRUD operations for UmrahTaskComplete table using Generic Repository Pattern
    /// </summary>
    public class UmrahTaskCompleteRepository : RepositoryBase<UmrahTaskComplete>, IUmrahTaskCompleteRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public UmrahTaskCompleteRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public UmrahTaskCompleteRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        /// <summary>
        /// To add a new record in UmrahTaskComplete table
        /// </summary>
        /// <param name="umrahTaskComplete">Instance of UmrahTaskComplete class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddUmrahTaskComplete(UmrahTaskComplete umrahTaskComplete)
        {
            try
            {
                if (umrahTaskComplete == null)
                    return false;
                _dbContext.UmrahTaskCompletes.Add(umrahTaskComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the UmrahTaskComplete table
        /// </summary>
        /// <param name="umrahTaskCompleteId">The instance of UmrahTaskComplete class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteUmrahTaskComplete(int umrahTaskCompleteId)
        {
            try
            {
                if (umrahTaskCompleteId == 0)
                    return false;
                var deleteUmrahTaskComplete = _dbContext.UmrahTaskCompletes.FirstOrDefault(emp => emp.UmrahTaskCompleteId == umrahTaskCompleteId);
                _dbContext.UmrahTaskCompletes.Remove(deleteUmrahTaskComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using UmrahTaskComplete ID from Dua table
        /// </summary>
        /// <param name="umrahTaskCompleteId">Primary Key of the row (UmrahTaskComplete ID)</param>
        /// <returns>Returns a Dua row matching the passing ID</returns>
        public UmrahTaskComplete GetUmrahTaskCompleteById(int umrahTaskCompleteId)
        {
            try
            {
                if (Equals(umrahTaskCompleteId, 0))
                    return null;

                return _dbContext.UmrahTaskCompletes.AsNoTracking().Where(x => x.UmrahTaskCompleteId == umrahTaskCompleteId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from UmrahTaskComplete table
        /// </summary>
        /// <returns>An IQuerable List of UmrahTaskComplete</returns>
        public IEnumerable<UmrahTaskComplete> GetAllUmrahTaskComplete()
        {
            try
            {
                return _dbContext.UmrahTaskCompletes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all records using UmrahTaskComplete Username of the UmrahTaskComplete table
        /// </summary>
        /// <param name="UmrahTaskCompleteUserId">UmrahTaskComplete Username</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public IEnumerable<UmrahTaskComplete> GetAllUmrahTaskCompleteByUser(string UmrahTaskCompleteUserId)
        {
            try
            {
                var data = _dbContext.UmrahTaskCompletes.Where(x => x.UmrahTaskCompleteByUserId == UmrahTaskCompleteUserId).ToList();
                if (Equals(UmrahTaskCompleteUserId, null))
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