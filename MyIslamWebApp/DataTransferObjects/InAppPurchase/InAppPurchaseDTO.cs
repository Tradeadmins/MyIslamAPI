using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.InAppPurchase
{
    public class InAppPurchaseDTO
    {
        public int InAppPurchaseId { get; set; }
        public string InAppPurchaseByUserId { get; set; }
        public decimal InAppPurchaseTotalAmount { get; set; }
        public decimal InAppPurchaseOwnerAmount { get; set; }
        public decimal InAppPurchaseUserAmount { get; set; }
        public decimal InAppPurchaseUserLocalAmount { get; set; }
        public string  InAppPurchaseUserLocalCurrencyType { get; set; }
    }
}