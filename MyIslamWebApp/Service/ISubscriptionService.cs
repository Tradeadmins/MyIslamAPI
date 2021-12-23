using System;
using MyIslamWebApp.DataTransferObjects.Result;
using MyIslamWebApp.DataTransferObjects.Subscription;

namespace MyIslamWebApp.Service
{
    public interface ISubscriptionService
    {
        //Subscription CRUD Operations
        bool AddSubscription(SubscriptionDTO subscription, string userId);
        bool DeleteSubscriptionById(int subscriptionID, string userId);
        bool UpdateSubscription(SubscriptionDTO subscription, string userId);
        bool DeleteSubscription(SubscriptionDTO subscription, string userId);     
        SubscriptionDTO GetSubscriptionById(int subscriptionId);
        bool GetSubscriptionByUserId(string userId);
        ResultDTO<SubscriptionDTO> GetAllSubscriptions(int pageIndex, int pageSize);
    }
}