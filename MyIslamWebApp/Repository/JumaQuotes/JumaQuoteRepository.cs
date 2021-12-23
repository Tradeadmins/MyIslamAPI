using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Enums;
using System.Data.Entity;

namespace MyIslamWebApp.Repository.JumaQuotes
{ /// <summary>
  /// CRUD operations for JumaQuote table using Generic Repository Pattern
  /// </summary>
    public class JumaQuoteRepository : RepositoryBase<JumaQuote>, IJumaQuoteRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public JumaQuoteRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public JumaQuoteRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in JumaQuote table
        /// </summary>
        /// <param name="jumaQuote">Instance of JumaQuote class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddJumaQuote(JumaQuote jumaQuote)
        {
            try
            {
                if (jumaQuote == null)
                    return false;

                _dbContext.JumaQuotes.Add(jumaQuote);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in JumaQuote table
        /// </summary>
        /// <param name="jumaQuote">The instance of JumaQuote class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateJumaQuote(JumaQuote jumaQuote)
        {
            try
            {
                if (jumaQuote == null)
                    return false;

                _dbContext.Entry(jumaQuote).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the JumaQuote table
        /// </summary>
        /// <param name="jumaQuote">The instance of JumaQuote class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteJumaQuote(JumaQuote jumaQuote)
        {
            try
            {
                if (jumaQuote == null)
                    return false;

                _dbContext.Entry(jumaQuote).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using JumaQuote ID of the JumaQuote table
        /// </summary>
        /// <param name="jumaQuoteId">Primary Key of the row (JumaQuote ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteJumaQuoteById(int jumaQuoteId)
        {
            try
            {
                if (int.Equals(jumaQuoteId, 0))
                    return false;

                JumaQuote record = _dbContext.JumaQuotes.Find(jumaQuoteId);

                if (record == null)
                    return false;

                _dbContext.JumaQuotes.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from JumaQuote table
        /// </summary>
        /// <returns>An IQuerable List of JumaQuote</returns>
        public Result<JumaQuote> GetAllJumaQuotes(int pageIndex, int pageSize)
        {
            try
            {
                Result<JumaQuote> jumaQuoteList = new Result<JumaQuote>();
                var count = _dbContext.Duas.Where(x => x.IsActive == true).Count();
                if (count > 0)
                {
                    jumaQuoteList.Response = _dbContext.JumaQuotes.Where(x => x.IsActive == true).OrderBy(x => x.JumaQuoteId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    jumaQuoteList.TotalCount = _dbContext.JumaQuotes.Where(x => x.IsActive == true).Count();
                }
                else
                {
                    jumaQuoteList.Response = _dbContext.JumaQuotes.Where(x => x.IsActive == true).ToList();
                    jumaQuoteList.TotalCount = _dbContext.JumaQuotes.Where(x => x.IsActive == true).Count();
                }
                return jumaQuoteList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using JumaQuote ID from JumaQuote table
        /// </summary>
        /// <param name="jumaQuoteId">Primary Key of the row (JumaQuote ID)</param>
        /// <returns>Returns a JumaQuote row matching the passing ID</returns>
        public JumaQuote GetJumaQuoteById(int jumaQuoteId)
        {
            try
            {
                if (Equals(jumaQuoteId, 0))
                    return null;

                return _dbContext.JumaQuotes.AsNoTracking().Where(x => x.JumaQuoteId == jumaQuoteId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using JumaQuote ID from JumaQuote table
        /// </summary>
        /// <param name="languagecode"></param>
        /// <param name="utcDateTime"></param>
        /// <returns>Returns a JumaQuote row matching the passing ID</returns>
        public JumaQuote GetJumaQuoteByLang_Date(int languagecode, DateTime utcDateTime)
        {
            try
            {
                AppLangauges language = (AppLangauges)languagecode;
                JumaQuote data = _dbContext.JumaQuotes.Where(x => x.JumaQuoteLanguage == language && x.JumaQuoteValidOn == utcDateTime).FirstOrDefault();
                if (Equals(languagecode))
                    return new JumaQuote();
                //.Find(languagecode, utcDateTime);
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