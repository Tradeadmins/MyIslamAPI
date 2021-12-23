using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.Dua;

namespace MyIslamWebApp.Repository.Duas
{
    /// <summary>
    /// CRUD operations for Dua table using Generic Repository Pattern
    /// </summary>
    public class DuaRepository : RepositoryBase<Dua>, IDuaRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public DuaRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public DuaRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in Dua table
        /// </summary>
        /// <param name="dua">Instance of Dua class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddDua(Dua dua)
        {
            try
            {
                if (dua == null)
                    return false;

                _dbContext.Duas.Add(dua);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in Dua table
        /// </summary>
        /// <param name="dua">The instance of Dua class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateDua(Dua dua)
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
        /// To delete a record from the Dua table
        /// </summary>
        /// <param name="dua">The instance of Dua class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDua(Dua dua)
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
        /// To delete a record using Dua ID of the Dua table
        /// </summary>
        /// <param name="duaId">Primary Key of the row (Dua ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDuaById(int duaId)
        {
            try
            {
                if (int.Equals(duaId, 0))
                    return false;

                Dua record = _dbContext.Duas.Find(duaId);

                if (record == null)
                    return false;

                _dbContext.Duas.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Dua ID from Dua table
        /// </summary>
        /// <param name="duaId">Primary Key of the row (Dua ID)</param>
        /// <returns>Returns a Dua row matching the passing ID</returns>
        public Dua GetDuaById(int duaId)
        {
            try
            {
                if (Equals(duaId, 0))
                    return null;

                return _dbContext.Duas.AsNoTracking().Where(x => x.DuaId == duaId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// To get all data from Dua table
        /// </summary>
        /// <returns>An IQuerable List of Dua</returns>
        public Result<Dua> GetAllDuas(int pageIndex, int pageSize)
        {
            try
            {
                Result<Dua> duasList = new Result<Dua>();
                var count = _dbContext.Duas.Where(x => x.IsActive == true).Count();
                if (count > 0)
                {
                    duasList.Response = _dbContext.Duas.Where(x => x.IsActive == true).OrderBy(x => x.DuaId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    duasList.TotalCount = _dbContext.Duas.Where(x => x.IsActive == true).Count();
                }
                else
                {
                    duasList.Response = _dbContext.Duas.Where(x => x.IsActive == true).ToList();
                    duasList.TotalCount = _dbContext.Duas.Where(x => x.IsActive == true).Count();
                }
                return duasList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all record in PrayerRequest table
        /// </summary>
        /// <param name="duaCategoryId">The instance of PrayerRequest class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public IEnumerable<DuaDTO> GetAllDuaByCategoryId(int duaCategoryId)
        {
            try
            {
                var dua = (from duaData in _dbContext.Duas
                           where duaData.DuaCategoryId == duaCategoryId && duaData.IsActive == true  // (Additional filtering criteria here...)
                           select new DuaDTO
                           {
                               DuaId = duaData.DuaId,
                               DuaCategoryId = duaData.DuaCategoryId,
                               DuaName = duaData.DuaName,
                               DuaArabicText = duaData.DuaArabicText,
                               DuaEnglishText = duaData.DuaEnglishText,
                               DuaTurkeyText = duaData.DuaTurkeyText,
                               DuaMalayText = duaData.DuaMalayText,
                               DuaPronunciationText = duaData.DuaPronunciationText,

                           }).ToList();
                return dua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}