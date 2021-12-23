using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Repository.InAppPurchases
{

    public interface IInAppPurchaseRepository : IRepository<InAppPurchase>
    {
        bool AddInAppPurchase(InAppPurchase inAppPurchase);
        bool DeleteInAppPurchase(InAppPurchase inAppPurchase);
        bool DeleteInAppPurchaseById(int inAppPurchaseId);
        bool UpdateInAppPurchase(InAppPurchase inAppPurchase);
        InAppPurchase GetInAppPurchaseById(int inAppPurchaseId);
        Result<InAppPurchase> GetAllInAppPurchases(int pageIndex, int pageSize);
    }
}