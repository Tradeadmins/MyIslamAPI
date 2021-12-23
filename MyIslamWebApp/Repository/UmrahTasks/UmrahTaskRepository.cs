using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.UmrahTask;

namespace MyIslamWebApp.Repository.UmrahTasks
{
    /// <summary>
    /// CRUD operations for UmrahTask table using Generic Repository Pattern
    /// </summary>
    public class UmrahTaskRepository : RepositoryBase<UmrahTask>, IUmrahTaskRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public UmrahTaskRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public UmrahTaskRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in UmrahTask table
        /// </summary>
        /// <param name="umrahTask">Instance of UmrahTask class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddUmrahTask(UmrahTask umrahTask)
        {
            try
            {
                if (umrahTask == null)
                    return false;

                _dbContext.UmrahTasks.Add(umrahTask);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in UmrahTask table
        /// </summary>
        /// <param name="umrahTask">The instance of UmrahTask class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateUmrahTask(UmrahTask umrahTask)
        {
            try
            {
                if (umrahTask == null)
                    return false;

                _dbContext.Entry(umrahTask).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the UmrahTask table
        /// </summary>
        /// <param name="umrahTask">The instance of UmrahTask class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteUmrahTask(UmrahTask umrahTask)
        {
            try
            {
                if (umrahTask == null)
                    return false;

                _dbContext.Entry(umrahTask).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using UmrahTask ID of the UmrahTask table
        /// </summary>
        /// <param name="umrahTaskId">Primary Key of the row (UmrahTask ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteUmrahTaskById(int umrahTaskId)
        {
            try
            {
                if (int.Equals(umrahTaskId, 0))
                    return false;

                UmrahTask record = _dbContext.UmrahTasks.Find(umrahTaskId);

                if (record == null)
                    return false;

                _dbContext.UmrahTasks.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using UmrahTask ID from UmrahTask table
        /// </summary>
        /// <param name="umrahTaskId">Primary Key of the row (UmrahTask ID)</param>
        /// <returns>Returns a UmrahTask row matching the passing ID</returns>
        public UmrahTask GetUmrahTaskById(int umrahTaskId)
        {
            try
            {
                if (Equals(umrahTaskId, 0))
                    return null;

                return _dbContext.UmrahTasks.AsNoTracking().Where(x => x.UmrahTaskId == umrahTaskId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from UmrahTask table
        /// </summary>
        /// <returns>An IQuerable List of UmrahTask</returns>
        public Result<UmrahTaskListDTO> GetAllUmrahTasks(int pageIndex, int pageSize, string userId)
        {
            try
            {
                Result<UmrahTaskListDTO> umrahTaskList = new Result<UmrahTaskListDTO>();

                var _umrahTaskList = (from umrahTasks in _dbContext.UmrahTasks
                                     where umrahTasks.IsActive == true  // (Additional filtering criteria here...)
                                     select new UmrahTaskListDTO
                                     {
                                         UmrahTaskId = umrahTasks.UmrahTaskId,
                                         UmrahTaskName = umrahTasks.UmrahTaskName,
                                         UmrahTaskCompleteId = _dbContext.UmrahTaskCompletes.Where(x => x.UmrahTaskCompleteByUserId == userId && x.UmrahTaskId == umrahTasks.UmrahTaskId).FirstOrDefault() != null ? _dbContext.UmrahTaskCompletes.Where(x => x.UmrahTaskCompleteByUserId == userId && x.UmrahTaskId == umrahTasks.UmrahTaskId).FirstOrDefault().UmrahTaskCompleteId : 0,
                                         UmrahTaskIsCompleted = _dbContext.UmrahTaskCompletes.Where(x => x.UmrahTaskCompleteByUserId == userId && x.UmrahTaskId == umrahTasks.UmrahTaskId).Count() > 0 ? true : false
                                     })
                                    .OrderBy(x => x.UmrahTaskId)
                                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                umrahTaskList.Response = _umrahTaskList;
                umrahTaskList.TotalCount = _dbContext.UmrahTasks.Where(x => x.IsActive == true).Count();

                return umrahTaskList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}