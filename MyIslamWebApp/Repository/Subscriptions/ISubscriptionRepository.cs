using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Repository.Subscriptions
{

    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        bool AddSubscription(Subscription subscription);
        bool DeleteSubscription(Subscription subscription);
        bool DeleteSubscriptionById(int subscriptionId);
        bool UpdateSubscription(Subscription subscription);
        Subscription GetSubscriptionById(int subscriptionId);
        bool GetSubscriptionByUserId(string userId);
        Result<Subscription> GetAllSubscriptions(int pageIndex, int pageSize);
    }
}