using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.UmrahGuide;

namespace MyIslamWebApp.Repository.UmrahGuides
{
    /// <summary>
    /// CRUD operations for UmrahGuide table using Generic Repository Pattern
    /// </summary>
    public class UmrahGuideRepository : RepositoryBase<UmrahGuide>, IUmrahGuideRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public UmrahGuideRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public UmrahGuideRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in UmrahGuide table
        /// </summary>
        /// <param name="umrahGuide">Instance of UmrahGuide class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddUmrahGuide(UmrahGuide umrahGuide)
        {
            try
            {
                if (umrahGuide == null)
                    return false;

                _dbContext.UmrahGuides.Add(umrahGuide);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in UmrahGuide table
        /// </summary>
        /// <param name="umrahGuide">The instance of UmrahGuide class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateUmrahGuide(UmrahGuide umrahGuide)
        {
            try
            {
                if (umrahGuide == null)
                    return false;

                _dbContext.Entry(umrahGuide).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the UmrahGuide table
        /// </summary>
        /// <param name="umrahGuide">The instance of UmrahGuide class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteUmrahGuide(UmrahGuide umrahGuide)
        {
            try
            {
                if (umrahGuide == null)
                    return false;

                _dbContext.Entry(umrahGuide).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using UmrahGuide ID of the UmrahGuide table
        /// </summary>
        /// <param name="umrahGuideId">Primary Key of the row (UmrahGuide ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteUmrahGuideById(int umrahGuideId)
        {
            try
            {
                if (int.Equals(umrahGuideId, 0))
                    return false;

                UmrahGuide record = _dbContext.UmrahGuides.Find(umrahGuideId);

                if (record == null)
                    return false;

                _dbContext.UmrahGuides.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the UmrahGuideComplete table
        /// </summary>
        /// <param name="umrahGuideCompleteId">The instance of UmrahGuideComplete class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteUmrahGuideComplete(int umrahGuideCompleteId)
        {
            try
            {
                if (umrahGuideCompleteId == 0)
                    return false;
                var deleteUmrahGuideComplete = _dbContext.UmrahGuideCompletes.FirstOrDefault(emp => emp.UmrahGuideCompleteId == umrahGuideCompleteId);
                _dbContext.UmrahGuideCompletes.Remove(deleteUmrahGuideComplete);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using UmrahGuide ID from UmrahGuide table
        /// </summary>
        /// <param name="umrahGuideId">Primary Key of the row (UmrahGuide ID)</param>
        /// <returns>Returns a UmrahGuide row matching the passing ID</returns>
        public UmrahGuide GetUmrahGuideById(int umrahGuideId)
        {
            try
            {
                if (Equals(umrahGuideId, 0))
                    return null;

                return _dbContext.UmrahGuides.AsNoTracking().Where(x => x.UmrahGuideId == umrahGuideId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from UmrahGuide table
        /// </summary>
        /// <returns>An IQuerable List of UmrahGuide</returns>
        public Result<UmrahGuideListDTO> GetAllUmrahGuides(int pageIndex, int pageSize, string userId)
        {
            try
            {
                Result<UmrahGuideListDTO> umrahGuideList = new Result<UmrahGuideListDTO>();

                var _umrahGuideList = (from umrahGuides in _dbContext.UmrahGuides
                                      where umrahGuides.IsActive == true  // (Additional filtering criteria here...)
                                      select new UmrahGuideListDTO
                                      {
                                          UmrahGuideId = umrahGuides.UmrahGuideId,
                                          UmrahGuideName = umrahGuides.UmrahGuideName,
                                          UmrahGuideIsCompleted = _dbContext.UmrahGuideCompletes.Where(x => x.UmrahGuideCompleteByUserId == userId && x.UmrahGuideId == umrahGuides.UmrahGuideId).Count() > 0 ? true : false
                                      })
                                    .OrderBy(x => x.UmrahGuideId)
                                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                umrahGuideList.Response = _umrahGuideList;
                umrahGuideList.TotalCount = _dbContext.UmrahGuides.Where(x => x.IsActive == true).Count();

                return umrahGuideList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}