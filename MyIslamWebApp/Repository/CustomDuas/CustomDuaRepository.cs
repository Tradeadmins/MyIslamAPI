using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.CustomDua;

namespace MyIslamWebApp.Repository.CustomDuas
{
    /// <summary>
    /// CRUD operations for CustomDua table using Generic Repository Pattern
    /// </summary>
    public class CustomDuaRepository : RepositoryBase<CustomDua>, ICustomDuaRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public CustomDuaRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public CustomDuaRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in CustomDua table
        /// </summary>
        /// <param name="dua">Instance of CustomDua class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddCustomDua(CustomDua dua)
        {
            try
            {
                if (dua == null)
                    return false;

                _dbContext.CustomDuas.Add(dua);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in CustomDua table
        /// </summary>
        /// <param name="dua">The instance of CustomDua class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateCustomDua(CustomDua dua)
        {
            try
            {
                if (dua == null)
                    return false;

                _dbContext.Entry(dua).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the CustomDua table
        /// </summary>
        /// <param name="dua">The instance of CustomDua class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteCustomDua(CustomDua dua)
        {
            try
            {
                if (dua == null)
                    return false;

                _dbContext.Entry(dua).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using CustomDua ID of the CustomDua table
        /// </summary>
        /// <param name="duaId">Primary Key of the row (CustomDua ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteCustomDuaById(int duaId)
        {
            try
            {
                if (int.Equals(duaId, 0))
                    return false;

                CustomDua record = _dbContext.CustomDuas.Find(duaId);

                if (record == null)
                    return false;

                _dbContext.CustomDuas.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using CustomDua ID from CustomDua table
        /// </summary>
        /// <param name="duaId">Primary Key of the row (CustomDua ID)</param>
        /// <returns>Returns a CustomDua row matching the passing ID</returns>
        public CustomDua GetCustomDuaById(int duaId)
        {
            try
            {
                if (Equals(duaId, 0))
                    return null;

                return _dbContext.CustomDuas.AsNoTracking().Where(x => x.CustomDuaId == duaId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from CustomDua table
        /// </summary>
        /// <returns>An IQuerable List of CustomDua</returns>
        public Result<CustomDua> GetAllCustomDuas(int pageIndex, int pageSize)
        {
            try
            {             
                Result<CustomDua> customDuasList = new Result<CustomDua>();

                var count = _dbContext.CustomDuas.Where(x => x.IsActive == true).Count();
                if (count > 0)
                {
                    customDuasList.Response = _dbContext.CustomDuas.Where(x => x.IsActive == true).OrderBy(x => x.CustomDuaId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    customDuasList.TotalCount = _dbContext.CustomDuas.Where(x => x.IsActive == true).Count();
                }
                else
                {
                    customDuasList.Response = _dbContext.CustomDuas.Where(x => x.IsActive == true).ToList();
                    customDuasList.TotalCount = _dbContext.MyEvents.Where(x => x.IsActive == true).Count();
                }
                return customDuasList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all record in CustomDua table
        /// </summary>
        /// <param name="userId">The instance of CustomDua class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public IEnumerable<CustomDuaDTO> GetAllCustomDuaByUserId(string userId)
        {
            try
            {
                var customDuas = (from customDua in _dbContext.CustomDuas
                                         where customDua.CreatedBy == userId && customDua.IsActive == true
                                         select new CustomDuaDTO
                                         {
                                             CustomDuaId = customDua.CustomDuaId,
                                             CustomDuaName = customDua.CustomDuaName,
                                             CustomDuaText = customDua.CustomDuaText,
                                         })
                                    .OrderBy(x => x.CustomDuaId)
                                    .ToList();

                return customDuas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}