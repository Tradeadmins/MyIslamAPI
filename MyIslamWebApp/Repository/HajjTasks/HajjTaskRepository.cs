using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.HajjTask;

namespace MyIslamWebApp.Repository.HajjTasks
{
    /// <summary>
    /// CRUD operations for HajjTask table using Generic Repository Pattern
    /// </summary>
    public class HajjTaskRepository : RepositoryBase<HajjTask>, IHajjTaskRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public HajjTaskRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public HajjTaskRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in HajjTask table
        /// </summary>
        /// <param name="hajjTask">Instance of HajjTask class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddHajjTask(HajjTask hajjTask)
        {
            try
            {
                if (hajjTask == null)
                    return false;

                _dbContext.HajjTasks.Add(hajjTask);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in HajjTask table
        /// </summary>
        /// <param name="hajjTask">The instance of HajjTask class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateHajjTask(HajjTask hajjTask)
        {
            try
            {
                if (hajjTask == null)
                    return false;

                _dbContext.Entry(hajjTask).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the HajjTask table
        /// </summary>
        /// <param name="hajjTask">The instance of HajjTask class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteHajjTask(HajjTask hajjTask)
        {
            try
            {
                if (hajjTask == null)
                    return false;

                _dbContext.Entry(hajjTask).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using HajjTask ID of the HajjTask table
        /// </summary>
        /// <param name="hajjTaskId">Primary Key of the row (HajjTask ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteHajjTaskById(int hajjTaskId)
        {
            try
            {
                if (int.Equals(hajjTaskId, 0))
                    return false;

                HajjTask record = _dbContext.HajjTasks.Find(hajjTaskId);

                if (record == null)
                    return false;

                _dbContext.HajjTasks.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using HajjTask ID from HajjTask table
        /// </summary>
        /// <param name="hajjTaskId">Primary Key of the row (HajjTask ID)</param>
        /// <returns>Returns a HajjTask row matching the passing ID</returns>
        public HajjTask GetHajjTaskById(int hajjTaskId)
        {
            try
            {
                if (Equals(hajjTaskId, 0))
                    return null;

                return _dbContext.HajjTasks.AsNoTracking().Where(x => x.HajjTaskId == hajjTaskId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from HajjTask table
        /// </summary>
        /// <returns>An IQuerable List of HajjTask</returns>
        public Result<HajjTaskListDTO> GetAllHajjTasks(int pageIndex, int pageSize, string userId)
        {
            try
            {
                Result<HajjTaskListDTO> hajjTaskList = new Result<HajjTaskListDTO>();

                var _hajjTaskList = (from hajjTasks in _dbContext.HajjTasks
                                          where hajjTasks.IsActive == true  // (Additional filtering criteria here...)
                                          select new HajjTaskListDTO
                                          {
                                              HajjTaskId = hajjTasks.HajjTaskId,
                                              HajjTaskName = hajjTasks.HajjTaskName,
                                              HajjTaskIsCompleted = _dbContext.HajjTaskCompletes.Where(x => x.HajjTaskCompleteByUserId == userId && x.HajjTaskId == hajjTasks.HajjTaskId).Count() > 0 ? true : false,
                                              HajjTaskCompleteId= _dbContext.HajjTaskCompletes.Where(x => x.HajjTaskCompleteByUserId == userId && x.HajjTaskId == hajjTasks.HajjTaskId).FirstOrDefault() !=null ? _dbContext.HajjTaskCompletes.Where(x => x.HajjTaskCompleteByUserId == userId && x.HajjTaskId == hajjTasks.HajjTaskId).FirstOrDefault().HajjTaskCompleteId : 0,
                                          })
                                    .OrderBy(x => x.HajjTaskId)
                                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                hajjTaskList.Response = _hajjTaskList;
                hajjTaskList.TotalCount = _dbContext.HajjTasks.Where(x => x.IsActive == true).Count();

                return hajjTaskList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     

        #endregion
    }
}