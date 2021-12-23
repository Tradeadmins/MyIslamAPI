using MyIslamWebApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.Subscription
{
    public class SubscriptionDTO
    {       
        public int SubscriptionId { get; set; }
        public string SubscriptionByUserId { get; set; }
        public SubscriptionTypes SubscriptionType { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public bool SubscriptionComplete { get; set; }
    }
}