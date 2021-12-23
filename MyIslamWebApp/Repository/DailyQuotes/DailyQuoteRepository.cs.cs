using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Repository.DailyQuotes
{
    /// <summary>
    /// CRUD operations for DailyQuote table using Generic Repository Pattern
    /// </summary>
    public class DailyQuoteRepository : RepositoryBase<DailyQuote>, IDailyQuoteRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public DailyQuoteRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public DailyQuoteRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in Daily Quote table
        /// </summary>
        /// <param name="dailyQuote">Instance of DailyQuote class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddDailyQuote(DailyQuote dailyQuote)
        {
            try
            {
                if (dailyQuote == null)
                    return false;

                _dbContext.DailyQuotes.Add(dailyQuote);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in DailyQuote table
        /// </summary>
        /// <param name="dailyQuote">The instance of DailyQuote class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateDailyQuote(DailyQuote dailyQuote)
        {
            try
            {
                if (dailyQuote == null)
                    return false;

                _dbContext.Entry(dailyQuote).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the DailyQuote table
        /// </summary>
        /// <param name="dailyQuote">The instance of DailyQuote class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDailyQuote(DailyQuote dailyQuote)
        {
            try
            {
                if (dailyQuote == null)
                    return false;

                //var deleteDailyQuote = _dbContext.DailyQuotes.FirstOrDefault(emp => emp.DailyQuoteId == dailyQuote.DailyQuoteId);
                //_dbContext.DailyQuotes.Remove(deleteDailyQuote);
                _dbContext.Entry(dailyQuote).State = EntityState.Modified;
                
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using DailyQuote ID of the DailyQuote table
        /// </summary>
        /// <param name="dailyQuoteId">Primary Key of the row (DailyQuote ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDailyQuoteById(int dailyQuoteId)
        {
            try
            {
                if (int.Equals(dailyQuoteId, 0))
                    return false;

                DailyQuote record = _dbContext.DailyQuotes.Find(dailyQuoteId);

                if (record == null)
                    return false;

                _dbContext.DailyQuotes.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using DailyQuote ID from DailyQuote table
        /// </summary>
        /// <param name="dailyQuoteId">Primary Key of the row (DailyQuote ID)</param>
        /// <returns>Returns a DailyQuote row matching the passing ID</returns>
        public DailyQuote GetDailyQuoteById(int dailyQuoteId)
        {
            try
            {
                if (Equals(dailyQuoteId, 0))
                    return null;

                return _dbContext.DailyQuotes.AsNoTracking().Where(x => x.DailyQuoteId == dailyQuoteId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from DailyQuote table
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public Result<DailyQuote> GetAllDailyQuotes(int pageIndex, int pageSize)
        {
            try                 
            {
                Result<DailyQuote> dailyQuotesList = new Result<DailyQuote>();
                var count = _dbContext.DailyQuotes.Where(x => x.IsActive == true).Count();
                if (count > 0)
                {
                    dailyQuotesList.Response = _dbContext.DailyQuotes.Where(x => x.IsActive == true).OrderBy(x => x.DailyQuoteValidOn).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    dailyQuotesList.TotalCount = _dbContext.DailyQuotes.Where(x => x.IsActive == true).Count();
                }
                else
                {
                    dailyQuotesList.Response = _dbContext.DailyQuotes.Where(x => x.IsActive == true).ToList();
                    dailyQuotesList.TotalCount = _dbContext.DailyQuotes.Where(x => x.IsActive == true).Count();
                }
                return dailyQuotesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using DailyQuote ID from DailyQuote table
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="utcDateTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Returns a DailyQuote row matching the passing ID</returns>
        public DailyQuote GetDailyQuoteByLang_Date(int languageCode, DateTime utcDateTime)
        {
            try
            {
                AppLangauges language = (AppLangauges)languageCode;
                DailyQuote data = _dbContext.DailyQuotes.Where(x => x.DailyQuoteLanguage == language && x.DailyQuoteValidOn == utcDateTime).FirstOrDefault();
                if (Equals(languageCode))
                return new DailyQuote();               
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