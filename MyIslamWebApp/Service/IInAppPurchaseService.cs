using MyIslamWebApp.DataTransferObjects.InAppPurchase;
using MyIslamWebApp.DataTransferObjects.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Service
{
    public interface IInAppPurchaseService
    {
        //InAppPurchase CRUD Operations
        ResultDTO<InAppPurchaseDTO> GetAllInAppPurchases(int pageIndex, int pageSize);
        InAppPurchaseDTO GetInAppPurchaseById(int inAppPurchaseId);
        bool AddInAppPurchase(InAppPurchaseDTO inAppPurchase, string userId);
        bool DeleteInAppPurchaseById(int inAppPurchaseID, string userId);
        bool UpdateInAppPurchase(InAppPurchaseDTO inAppPurchase, string userId);
        bool DeleteInAppPurchase(InAppPurchaseDTO inAppPurchase, string userId);
    }
}