using MyIslamWebApp.DataContext;
using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Repository.PrayerRequests
{
    /// <summary>
    /// CRUD operations for PrayerRequest table using Generic Repository Pattern
    /// </summary>
    public class PrayerRequestRepository : RepositoryBase<PrayerRequest>, IPrayerRequestRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public PrayerRequestRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public PrayerRequestRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in PrayerRequest table
        /// </summary>
        /// <param name="prayerRequest">Instance of PrayerRequest class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddPrayerRequest(PrayerRequest prayerRequest)
        {
            try
            {
                if (prayerRequest == null)
                    return false;

                _dbContext.PrayerRequests.Add(prayerRequest);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the PrayerRequest table
        /// </summary>
        /// <param name="prayerRequest">The instance of PrayerRequest class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeletePrayerRequest(PrayerRequest prayerRequest)
        {
            try
            {
                if (prayerRequest == null)
                    return false;

                _dbContext.Entry(prayerRequest).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using PrayerRequest ID of the PrayerRequest table
        /// </summary>
        /// <param name="prayerRequestId">Primary Key of the row (PrayerRequest ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeletePrayerRequestById(int prayerRequestId)
        {
            try
            {
                if (int.Equals(prayerRequestId, 0))
                    return false;

                PrayerRequest record = _dbContext.PrayerRequests.Find(prayerRequestId);

                if (record == null)
                    return false;

                _dbContext.PrayerRequests.Remove(record);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from PrayerRequest table
        /// </summary>
        /// <returns>An IQuerable List of PrayerRequest</returns>
        public Result<PrayerRequestListDTO> GetAllPrayerRequests(int pageIndex, int pageSize, string userId)
        {
            try
            {
                Result<PrayerRequestListDTO> prayerRequestList = new Result<PrayerRequestListDTO>();

                var _prayerRequestList = (from prayerRequest in _dbContext.PrayerRequests
                                          where prayerRequest.IsActive == true  // (Additional filtering criteria here...)
                                          select new PrayerRequestListDTO
                                          {
                                              PrayerRequestId = prayerRequest.PrayerRequestId,
                                              PrayerRequestText = prayerRequest.PrayerRequestText,
                                              PrayerRequestTotalDuaCount = _dbContext.MakeDuas.Where(x => x.MakeDuaPrayerRequestId == prayerRequest.PrayerRequestId).Count().ToString(),
                                              PrayerRequestIsLiked = _dbContext.MakeDuas.Where(x => x.MakeDuaByUserId == userId && x.MakeDuaPrayerRequestId == prayerRequest.PrayerRequestId).Count() > 0 ? true : false
                                          })
                                    .OrderBy(x => x.PrayerRequestId)
                                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                prayerRequestList.Response = _prayerRequestList;
                prayerRequestList.TotalCount = _dbContext.PrayerRequests.Where(x => x.IsActive == true).Count();

                return prayerRequestList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using PrayerRequest ID from PrayerRequest table
        /// </summary>
        /// <param name="prayerRequestId">Primary Key of the row (PrayerRequest ID)</param>
        /// <returns>Returns a PrayerRequest row matching the passing ID</returns>
        public PrayerRequest GetPrayerRequestById(int prayerRequestId)
        {
            try
            {
                if (Equals(prayerRequestId, 0))
                    return null;

                return _dbContext.PrayerRequests.AsNoTracking().Where(x => x.PrayerRequestId == prayerRequestId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in PrayerRequest table
        /// </summary>
        /// <param name="prayerRequest">The instance of PrayerRequest class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdatePrayerRequest(PrayerRequest prayerRequest)
        {
            try
            {
                if (prayerRequest == null)
                    return false;

                _dbContext.Entry(prayerRequest).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all record in PrayerRequest table
        /// </summary>
        /// <param name="prayerRequestUserId">The instance of PrayerRequest class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public Result<PrayerRequestListDTO> GetAllPrayerRequestByUserId(string prayerRequestUserId)
        {
            try
            {
                Result<PrayerRequestListDTO> prayerRequestList = new Result<PrayerRequestListDTO>();

                var _prayerRequestList = (from prayerRequest in _dbContext.PrayerRequests
                                          where prayerRequest.IsActive == true && prayerRequest.CreatedBy== prayerRequestUserId // (Additional filtering criteria here...)
                                          select new PrayerRequestListDTO
                                          {
                                              PrayerRequestId = prayerRequest.PrayerRequestId,
                                              PrayerRequestText = prayerRequest.PrayerRequestText,
                                              PrayerRequestTotalDuaCount = _dbContext.MakeDuas.Where(x => x.MakeDuaPrayerRequestId == prayerRequest.PrayerRequestId).Count().ToString(),
                                              PrayerRequestIsLiked = _dbContext.MakeDuas.Where(x => x.MakeDuaByUserId == prayerRequestUserId && x.MakeDuaPrayerRequestId == prayerRequest.PrayerRequestId).Count() > 0 ? true : false
                                          })
                                    .OrderBy(x => x.PrayerRequestId)
                                    .ToList();

                prayerRequestList.Response = _prayerRequestList;
                prayerRequestList.TotalCount = _prayerRequestList.Count();

                return prayerRequestList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}