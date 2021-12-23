using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.DuaCategorys
{
    /// <summary>
    /// CRUD operations for DuaCategory table using Generic Repository Pattern
    /// </summary>
    public class DuaCategoryRepository : RepositoryBase<DuaCategory>, IDuaCategoryRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public DuaCategoryRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public DuaCategoryRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in DuaCategory table
        /// </summary>
        /// <param name="duaCategory">Instance of DuaCategory class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddDuaCategory(DuaCategory duaCategory)
        {
            try
            {
                if (duaCategory == null)
                    return false;

                _dbContext.DuaCategorys.Add(duaCategory);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in DuaCategory table
        /// </summary>
        /// <param name="duaCategory">The instance of DuaCategory class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateDuaCategory(DuaCategory duaCategory)
        {
            try
            {
                if (duaCategory == null)
                    return false;

                _dbContext.Entry(duaCategory).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the DuaCategory table
        /// </summary>
        /// <param name="duaCategory">The instance of DuaCategory class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDuaCategory(DuaCategory duaCategory)
        {
            try
            {
                if (duaCategory == null)
                    return false;

                _dbContext.Entry(duaCategory).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using DuaCategory ID of the DuaCategory table
        /// </summary>
        /// <param name="duaCategoryId">Primary Key of the row (DuaCategory ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDuaCategoryById(int duaCategoryId)
        {
            try
            {
                if (int.Equals(duaCategoryId, 0))
                    return false;

                DuaCategory record = _dbContext.DuaCategorys.Find(duaCategoryId);

                if (record == null)
                    return false;

                _dbContext.DuaCategorys.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using DuaCategory ID from DuaCategory table
        /// </summary>
        /// <param name="duaCategoryId">Primary Key of the row (DuaCategory ID)</param>
        /// <returns>Returns a DuaCategory row matching the passing ID</returns>
        public DuaCategory GetDuaCategoryById(int duaCategoryId)
        {
            try
            {
                if (Equals(duaCategoryId, 0))
                    return null;

                return _dbContext.DuaCategorys.AsNoTracking().Where(x => x.DuaCategoryId == duaCategoryId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from DuaCategory table
        /// </summary>
        /// <returns>An IQuerable List of DuaCategory</returns>
        public IEnumerable<DuaCategory> GetAllDuaCategorys()
        {
            try
            {
                return _dbContext.DuaCategorys.Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}