using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Repository.Subscriptions
{
    /// <summary>
    /// CRUD operations for Subscription table using Generic Repository Pattern
    /// </summary>
    public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;    
        #endregion

        #region Constructor
        public SubscriptionRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();           
        }

        public SubscriptionRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in Daily Quote table
        /// </summary>
        /// <param name="subscription">Instance of Subscription class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddSubscription(Subscription subscription)
        {
            try
            {              
                {
                    if (subscription == null)
                        return false;                   
                   
                    _dbContext.Subscriptions.Add(subscription);
                        return true;                    
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in Subscription table
        /// </summary>
        /// <param name="subscription">The instance of Subscription class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateSubscription(Subscription subscription)
        {
            try
            {
                if (subscription == null)
                    return false;

                _dbContext.Entry(subscription).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the Subscription table
        /// </summary>
        /// <param name="subscription">The instance of Subscription class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteSubscription(Subscription subscription)
        {
            try
            {
                if (subscription == null)
                    return false;
                
                _dbContext.Entry(subscription).State = EntityState.Modified;

                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using Subscription ID of the Subscription table
        /// </summary>
        /// <param name="subscriptionId">Primary Key of the row (Subscription ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteSubscriptionById(int subscriptionId)
        {
            try
            {
                if (int.Equals(subscriptionId, 0))
                    return false;

                Subscription record = _dbContext.Subscriptions.Find(subscriptionId);

                if (record == null)
                    return false;

                _dbContext.Subscriptions.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Subscription ID from Subscription table
        /// </summary>
        /// <param name="subscriptionId">Primary Key of the row (Subscription ID)</param>
        /// <returns>Returns a Subscription row matching the passing ID</returns>
        public Subscription GetSubscriptionById(int subscriptionId)
        {
            try
            {
                if (Equals(subscriptionId, 0))
                    return null;

                return _dbContext.Subscriptions.AsNoTracking().Where(x => x.SubscriptionId == subscriptionId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from Subscription table
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public Result<Subscription> GetAllSubscriptions(int pageIndex, int pageSize)
        {
            try
            {
                Result<Subscription> subscriptionsList = new Result<Subscription>();
                var count = _dbContext.Subscriptions.Where(x => x.IsActive == true).Count();
                if (count > 0)
                {
                    subscriptionsList.Response = _dbContext.Subscriptions.Where(x => x.IsActive == true).OrderBy(x => x.SubscriptionId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    subscriptionsList.TotalCount = _dbContext.Subscriptions.Where(x => x.IsActive == true).Count();
                }
                else
                {
                    subscriptionsList.Response = _dbContext.Subscriptions.Where(x => x.IsActive == true).ToList();
                    subscriptionsList.TotalCount = _dbContext.Subscriptions.Where(x => x.IsActive == true).Count();
                }
                return subscriptionsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Subscription ID from Subscription table
        /// </summary>
        /// <param name="userId">UserId Key of the row (Subscription ID)</param>
        /// <returns>Returns a Subscription row matching the passing ID</returns>
        public bool GetSubscriptionByUserId(string userId)
        {
            try
            {
                if (Equals(userId, null))
                    return false;

                var data = _dbContext.Subscriptions.AsNoTracking().Where(x => x.SubscriptionByUserId == userId).FirstOrDefault();
                if (data == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}