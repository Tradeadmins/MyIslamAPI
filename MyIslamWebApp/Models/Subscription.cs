using MyIslamWebApp.Enums;
using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class Subscription : BaseModel
    {
        ///<summary>
        ///Id of the DailyQuote
        ///</summary>
        [Key]
        public int SubscriptionId { get; set; }
        ///<summary>
        ///Subscription By UserId
        ///</summary>
        [Required]
        public string SubscriptionByUserId { get; set; }
        ///<summary>
        ///Subscription Type
        /// </summary>
        [Required]
        public SubscriptionTypes SubscriptionType { get; set; }
        ///<summary>
        ///Subscription End Date
        ///</summary>
        [Required]
        public DateTime SubscriptionEndDate { get; set; }
        ///<summary>
        ///Subscription End Date
        ///</summary>
        [Required]
        public bool SubscriptionComplete { get; set; }
    }
}