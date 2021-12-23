using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.QuranTranslates
{
    /// <summary>
    /// CRUD operations for QuranTranslate table using Generic Repository Pattern
    /// </summary>
    public class QuranTranslateRepository : RepositoryBase<QuranTranslate>, IQuranTranslateRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public QuranTranslateRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public QuranTranslateRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in QuranTranslate table
        /// </summary>
        /// <param name="quranTranslate">Instance of QuranTranslate class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddQuranTranslate(QuranTranslate quranTranslate)
        {
            try
            {
                if (quranTranslate == null)
                    return false;

                _dbContext.QuranTranslates.Add(quranTranslate);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in QuranTranslate table
        /// </summary>
        /// <param name="quranTranslate">The instance of QuranTranslate class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateQuranTranslate(QuranTranslate quranTranslate)
        {
            try
            {
                if (quranTranslate == null)
                    return false;

                _dbContext.Entry(quranTranslate).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the QuranTranslate table
        /// </summary>
        /// <param name="quranTranslate">The instance of QuranTranslate class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteQuranTranslate(QuranTranslate quranTranslate)
        {
            try
            {
                if (quranTranslate == null)
                    return false;

                _dbContext.Entry(quranTranslate).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using QuranTranslate ID of the QuranTranslate table
        /// </summary>
        /// <param name="quranTranslateId">Primary Key of the row (QuranTranslate ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteQuranTranslateById(int quranTranslateId)
        {
            try
            {
                if (int.Equals(quranTranslateId, 0))
                    return false;

                QuranTranslate record = _dbContext.QuranTranslates.Find(quranTranslateId);

                if (record == null)
                    return false;

                _dbContext.QuranTranslates.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using QuranTranslate ID from QuranTranslate table
        /// </summary>
        /// <param name="quranTranslateId">Primary Key of the row (QuranTranslate ID)</param>
        /// <returns>Returns a QuranTranslate row matching the passing ID</returns>
        public QuranTranslate GetQuranTranslateById(int quranTranslateId)
        {
            try
            {
                if (Equals(quranTranslateId, 0))
                    return null;

                return _dbContext.QuranTranslates.AsNoTracking().Where(x => x.QuranTranslateId == quranTranslateId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from QuranTranslate table
        /// </summary>
        /// <returns>An IQuerable List of QuranTranslate</returns>
        public IEnumerable<QuranTranslate> GetAllQuranTranslates()
        {
            try
            {
                return _dbContext.QuranTranslates.Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}