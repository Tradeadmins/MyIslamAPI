using System;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Repository.InAppPurchases
{
    /// <summary>
    /// CRUD operations for InAppPurchase table using Generic Repository Pattern
    /// </summary>
    public class InAppPurchaseRepository : RepositoryBase<InAppPurchase>, IInAppPurchaseRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public InAppPurchaseRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public InAppPurchaseRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in Daily Quote table
        /// </summary>
        /// <param name="inAppPurchase">Instance of InAppPurchase class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddInAppPurchase(InAppPurchase inAppPurchase)
        {
            try
            {
                if (inAppPurchase == null)
                    return false;

                _dbContext.InAppPurchases.Add(inAppPurchase);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in InAppPurchase table
        /// </summary>
        /// <param name="inAppPurchase">The instance of InAppPurchase class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateInAppPurchase(InAppPurchase inAppPurchase)
        {
            try
            {
                if (inAppPurchase == null)
                    return false;

                _dbContext.Entry(inAppPurchase).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the InAppPurchase table
        /// </summary>
        /// <param name="inAppPurchase">The instance of InAppPurchase class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteInAppPurchase(InAppPurchase inAppPurchase)
        {
            try
            {
                if (inAppPurchase == null)
                    return false;

                //var deleteInAppPurchase = _dbContext.InAppPurchases.FirstOrDefault(emp => emp.InAppPurchaseId == inAppPurchase.InAppPurchaseId);
                //_dbContext.InAppPurchases.Remove(deleteInAppPurchase);
                _dbContext.Entry(inAppPurchase).State = EntityState.Modified;

                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using InAppPurchase ID of the InAppPurchase table
        /// </summary>
        /// <param name="inAppPurchaseId">Primary Key of the row (InAppPurchase ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteInAppPurchaseById(int inAppPurchaseId)
        {
            try
            {
                if (int.Equals(inAppPurchaseId, 0))
                    return false;

                InAppPurchase record = _dbContext.InAppPurchases.Find(inAppPurchaseId);

                if (record == null)
                    return false;

                _dbContext.InAppPurchases.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using InAppPurchase ID from InAppPurchase table
        /// </summary>
        /// <param name="inAppPurchaseId">Primary Key of the row (InAppPurchase ID)</param>
        /// <returns>Returns a InAppPurchase row matching the passing ID</returns>
        public InAppPurchase GetInAppPurchaseById(int inAppPurchaseId)
        {
            try
            {
                if (Equals(inAppPurchaseId, 0))
                    return null;

                return _dbContext.InAppPurchases.AsNoTracking().Where(x => x.InAppPurchaseId == inAppPurchaseId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from InAppPurchase table
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public Result<InAppPurchase> GetAllInAppPurchases(int pageIndex, int pageSize)
        {
            try
            {
                Result<InAppPurchase> inAppPurchasesList = new Result<InAppPurchase>();
                var count = _dbContext.InAppPurchases.Where(x => x.IsActive == true).Count();
                if (count > 0)
                {
                    inAppPurchasesList.Response = _dbContext.InAppPurchases.Where(x => x.IsActive == true).OrderBy(x => x.InAppPurchaseId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    inAppPurchasesList.TotalCount = _dbContext.InAppPurchases.Where(x => x.IsActive == true).Count();
                }
                else
                {
                    inAppPurchasesList.Response = _dbContext.InAppPurchases.Where(x => x.IsActive == true).ToList();
                    inAppPurchasesList.TotalCount = _dbContext.InAppPurchases.Where(x => x.IsActive == true).Count();
                }
                return inAppPurchasesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}