using System;
using System.Collections.Generic;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;
using MyIslamWebApp.DataTransferObjects.Subscription;
using MyIslamWebApp.Enums;
using MyIslamWebApp.DataContext;
using System.Linq;

namespace MyIslamWebApp.Service
{
    public class SubscriptionService : ISubscriptionService
    {     
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        public readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public SubscriptionService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Subscription Methods
        public bool AddSubscription(SubscriptionDTO subscription, string userId)
        {
            try
            {
                var result = false;
                #region Subscriptiomn
                var subscriptions = DateTime.Now.AddDays(14);
                if (subscription.SubscriptionType == SubscriptionTypes.Trail)
                {
                    subscriptions = DateTime.Now.AddDays(14);
                }
                else if (subscription.SubscriptionType == SubscriptionTypes.Monthly)
                {
                    subscriptions = DateTime.Now.AddDays(30);
                }
                else if (subscription.SubscriptionType == SubscriptionTypes.Yearly)
                {
                    subscriptions = DateTime.Now.AddDays(365);
                }
                var subscriptionEndDate = subscriptions.Date;
                #endregion

                if (subscription == null)
                    return result;

                var addSubscription = new Subscription();
                userId = subscription.SubscriptionByUserId;
                addSubscription.SubscriptionId = subscription.SubscriptionId;
                addSubscription.SubscriptionByUserId = subscription.SubscriptionByUserId;
                addSubscription.SubscriptionType = subscription.SubscriptionType;
                addSubscription.SubscriptionEndDate = subscriptionEndDate;
                addSubscription.SubscriptionComplete = subscription.SubscriptionComplete;

                var dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;

                //Default Values
                addSubscription.IsActive = true;
                addSubscription.CreatedBy = userId;
                addSubscription.CreatedOn = date;
                addSubscription.UpdatedBy = userId;
                addSubscription.UpdatedOn = date;
                result = _unitOfWork.subscriptionRepository.AddSubscription(addSubscription);
                _unitOfWork.Commit();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateSubscription(SubscriptionDTO subscription, string userId)
        {
            try
            {
                var subscriptionEndDate = DateTime.Now.AddDays(14);

                if (subscription.SubscriptionType == SubscriptionTypes.Trail)
                {
                    subscriptionEndDate = DateTime.Now.AddDays(14);
                }
                else if (subscription.SubscriptionType == SubscriptionTypes.Monthly)
                {
                    subscriptionEndDate = DateTime.Now.AddDays(30);
                }
                else if (subscription.SubscriptionType == SubscriptionTypes.Yearly)
                {
                    subscriptionEndDate = DateTime.Now.AddDays(365);
                }

                var result = false;
                if (subscription == null)
                    return result;

                var _subscriptions = _unitOfWork.subscriptionRepository.GetSubscriptionById(subscription.SubscriptionId);

                if (_subscriptions == null)
                    return result;

                var updateSubscription = new Subscription();
                updateSubscription.SubscriptionId = subscription.SubscriptionId;
                updateSubscription.SubscriptionType = subscription.SubscriptionType;
                updateSubscription.SubscriptionEndDate = subscription.SubscriptionEndDate;
              
                result = _unitOfWork.subscriptionRepository.UpdateSubscription(updateSubscription);

                ////Default Values
                updateSubscription.IsActive = _subscriptions.IsActive;
                updateSubscription.CreatedBy = _subscriptions.CreatedBy;
                updateSubscription.CreatedOn = _subscriptions.CreatedOn;
                updateSubscription.UpdatedBy = userId;
                updateSubscription.UpdatedOn = DateTime.Now;

                result = _unitOfWork.subscriptionRepository.UpdateSubscription(updateSubscription);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteSubscription(SubscriptionDTO subscription, string userId)
        {
            try
            {
                var result = false;
                if (subscription == null)
                    return result;

                var deleteSubscription = new Subscription();
                deleteSubscription.SubscriptionId = subscription.SubscriptionId;
                deleteSubscription.SubscriptionByUserId = subscription.SubscriptionByUserId;
                deleteSubscription.SubscriptionType = subscription.SubscriptionType;
                deleteSubscription.SubscriptionEndDate = subscription.SubscriptionEndDate;

                ////Default Values
                deleteSubscription.IsActive = false;
                deleteSubscription.UpdatedBy = userId;
                deleteSubscription.UpdatedOn = DateTime.Now;

                result = _unitOfWork.subscriptionRepository.DeleteSubscription(deleteSubscription);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteSubscriptionById(int subscriptionID, string userId)
        {
            try
            {
                var result = false;
                if (subscriptionID == 0)
                    return result;

                var subscription = _unitOfWork.subscriptionRepository.GetSubscriptionById(subscriptionID);

                if (subscription == null)
                    return result;

                var deletesubscription = new Subscription();
                deletesubscription = subscription;
                ////Default Values
                deletesubscription.IsActive = false;
                deletesubscription.UpdatedBy = userId;
                deletesubscription.UpdatedOn = DateTime.Now;

                result = _unitOfWork.subscriptionRepository.DeleteSubscription(deletesubscription);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SubscriptionDTO GetSubscriptionById(int subscriptionID)
        {
            try
            {
                SubscriptionDTO subscriptionResponse = new SubscriptionDTO();
                var subscription = _unitOfWork.subscriptionRepository.GetSubscriptionById(subscriptionID);
                if (subscription == null)
                    return null;

                if (subscription.IsActive)
                {
                    subscriptionResponse.SubscriptionId = subscription.SubscriptionId;
                    subscriptionResponse.SubscriptionByUserId = subscription.SubscriptionByUserId;
                    subscriptionResponse.SubscriptionType = subscription.SubscriptionType;
                    subscriptionResponse.SubscriptionEndDate = subscription.SubscriptionEndDate;
                }
                return subscriptionResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<SubscriptionDTO> GetAllSubscriptions(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<SubscriptionDTO>();
                var subscriptionsList = new List<SubscriptionDTO>();
                SubscriptionDTO subscriptionResponse;
                var allSubscriptions = _unitOfWork.subscriptionRepository.GetAllSubscriptions(pageIndex, pageSize);

                if (allSubscriptions.TotalCount == 0)
                    return response;

                foreach (var subscription in allSubscriptions.Response)
                {
                    if (subscription.IsActive)
                    {
                        subscriptionResponse = new SubscriptionDTO();
                        subscriptionResponse.SubscriptionId = subscription.SubscriptionId;
                        subscriptionResponse.SubscriptionByUserId = subscription.SubscriptionByUserId;
                        subscriptionResponse.SubscriptionType = subscription.SubscriptionType;
                        subscriptionResponse.SubscriptionEndDate = subscription.SubscriptionEndDate;
                        subscriptionsList.Add(subscriptionResponse);
                    }
                }
                response.Response = subscriptionsList;
                response.TotalCount = allSubscriptions.TotalCount;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetSubscriptionByUserId(string userId)
        {
            try
            {
                SubscriptionDTO subscriptionResponse = new SubscriptionDTO();
                var subscription = _unitOfWork.subscriptionRepository.GetSubscriptionByUserId(userId);
                if (subscription == false)
                    return false;
                else                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}