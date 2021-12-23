using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.HajjGuide;

namespace MyIslamWebApp.Repository.HajjGuides
{
    /// <summary>
    /// CRUD operations for HajjGuide table using Generic Repository Pattern
    /// </summary>
    public class HajjGuideRepository : RepositoryBase<HajjGuide>, IHajjGuideRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public HajjGuideRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public HajjGuideRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in HajjGuide table
        /// </summary>
        /// <param name="hajjGuide">Instance of HajjGuide class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddHajjGuide(HajjGuide hajjGuide)
        {
            try
            {
                if (hajjGuide == null)
                    return false;

                _dbContext.HajjGuides.Add(hajjGuide);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in HajjGuide table
        /// </summary>
        /// <param name="hajjGuide">The instance of HajjGuide class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateHajjGuide(HajjGuide hajjGuide)
        {
            try
            {
                if (hajjGuide == null)
                    return false;

                _dbContext.Entry(hajjGuide).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the HajjGuide table
        /// </summary>
        /// <param name="hajjGuide">The instance of HajjGuide class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteHajjGuide(HajjGuide hajjGuide)
        {
            try
            {
                if (hajjGuide == null)
                    return false;

                _dbContext.Entry(hajjGuide).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using HajjGuide ID of the HajjGuide table
        /// </summary>
        /// <param name="hajjGuideId">Primary Key of the row (HajjGuide ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteHajjGuideById(int hajjGuideId)
        {
            try
            {
                if (int.Equals(hajjGuideId, 0))
                    return false;

                HajjGuide record = _dbContext.HajjGuides.Find(hajjGuideId);

                if (record == null)
                    return false;

                _dbContext.HajjGuides.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using HajjGuide ID from HajjGuide table
        /// </summary>
        /// <param name="hajjGuideId">Primary Key of the row (HajjGuide ID)</param>
        /// <returns>Returns a HajjGuide row matching the passing ID</returns>
        public HajjGuide GetHajjGuideById(int hajjGuideId)
        {
            try
            {
                if (Equals(hajjGuideId, 0))
                    return null;

                return _dbContext.HajjGuides.AsNoTracking().Where(x => x.HajjGuideId == hajjGuideId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from HajjGuide table
        /// </summary>
        /// <returns>An IQuerable List of HajjGuide</returns>
        public Result<HajjGuideListDTO> GetAllHajjGuides(int pageIndex, int pageSize, string userId)
        {
            try
            {
                Result<HajjGuideListDTO> hajjGuideList = new Result<HajjGuideListDTO>();

                var _hajjGuideList = (from hajjGuides in _dbContext.HajjGuides
                                      //join hajjGuideCompletes in _dbContext.HajjGuideCompletes on hajjGuides.HajjGuideId equals hajjGuideCompletes.HajjGuideCompleteId
                                      where hajjGuides.IsActive == true  // (Additional filtering criteria here...)
                                     select new HajjGuideListDTO
                                     {
                                         HajjGuideId = hajjGuides.HajjGuideId,
                                         HajjGuideName = hajjGuides.HajjGuideName,
                                         HajjGuideCompleteId = _dbContext.HajjGuideCompletes.Where(x => x.HajjGuideCompleteByUserId == userId && x.HajjGuideId == hajjGuides.HajjGuideId).FirstOrDefault() != null ? _dbContext.HajjGuideCompletes.Where(x => x.HajjGuideCompleteByUserId == userId && x.HajjGuideId == hajjGuides.HajjGuideId).FirstOrDefault().HajjGuideCompleteId : 0,
                                         HajjGuideIsCompleted = _dbContext.HajjGuideCompletes.Where(x => x.HajjGuideCompleteByUserId == userId && x.HajjGuideId == hajjGuides.HajjGuideId).Count() > 0 ? true : false
                                     })
                                    .OrderBy(x => x.HajjGuideId)
                                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                hajjGuideList.Response = _hajjGuideList;
                hajjGuideList.TotalCount = _dbContext.HajjGuides.Where(x => x.IsActive == true).Count();

                return hajjGuideList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}