using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.Hadiths
{
    /// <summary>
    /// CRUD operations for Hadith table using Generic Repository Pattern
    /// </summary>
    public class HadithRepository : RepositoryBase<Hadith>, IHadithRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public HadithRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public HadithRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in Hadith table
        /// </summary>
        /// <param name="hadith">Instance of Hadith class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddHadith(Hadith hadith)
        {
            try
            {
                if (hadith == null)
                    return false;

                _dbContext.Hadiths.Add(hadith);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in Hadith table
        /// </summary>
        /// <param name="hadith">The instance of Hadith class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateHadith(Hadith hadith)
        {
            try
            {
                if (hadith == null)
                    return false;

                _dbContext.Entry(hadith).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the Hadith table
        /// </summary>
        /// <param name="hadith">The instance of Hadith class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteHadith(Hadith hadith)
        {
            try
            {
                if (hadith == null)
                    return false;

                _dbContext.Entry(hadith).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using Hadith ID of the Hadith table
        /// </summary>
        /// <param name="hadithId">Primary Key of the row (Hadith ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteHadithById(int hadithId)
        {
            try
            {
                if (int.Equals(hadithId, 0))
                    return false;

                Hadith record = _dbContext.Hadiths.Find(hadithId);

                if (record == null)
                    return false;

                _dbContext.Hadiths.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Hadith ID from Hadith table
        /// </summary>
        /// <param name="hadithId">Primary Key of the row (Hadith ID)</param>
        /// <returns>Returns a Hadith row matching the passing ID</returns>
        public Hadith GetHadithById(int hadithId)
        {
            try
            {
                if (Equals(hadithId, 0))
                    return null;

                return _dbContext.Hadiths.AsNoTracking().Where(x => x.HadithId == hadithId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from Hadith table
        /// </summary>
        /// <returns>An IQuerable List of Hadith</returns>
        public Result<Hadith> GetAllHadiths(int pageIndex, int pageSize)
        {
            try
            {
                Result<Hadith> hadithsList = new Result<Hadith>();
                var count = _dbContext.Hadiths.Where(x => x.IsActive == true).Count();
                if (count > 0)
                {
                    hadithsList.Response = _dbContext.Hadiths.Where(x => x.IsActive == true).OrderBy(x => x.HadithId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    hadithsList.TotalCount = _dbContext.Hadiths.Where(x => x.IsActive == true).Count();
                }
                else
                {
                    hadithsList.Response = _dbContext.Hadiths.Where(x => x.IsActive == true).ToList();
                    hadithsList.TotalCount = _dbContext.Hadiths.Where(x => x.IsActive == true).Count();
                }
                return hadithsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}