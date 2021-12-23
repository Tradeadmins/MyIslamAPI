using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class InAppPurchase : BaseModel
    {
        /// <summary>
        /// Id of the InAppPurchase
        /// </summary>
        [Key]
        public int InAppPurchaseId { get; set; }
        /// <summary>
        /// InAppPurchase's Total Amount
        /// </summary>
        [Required]
        public string InAppPurchaseByUserId { get; set; }
        /// <summary>
        /// InAppPurchase's Total Amount
        /// </summary>
        [Required]
        public decimal InAppPurchaseTotalAmount { get; set; }
        /// <summary>
        /// InAppPurchase's Owner Amount 
        /// </summary>
        [Required]
        public decimal InAppPurchaseOwnerAmount { get; set; }
        /// <summary>
        /// InAppPurchase's User Amount
        /// </summary>
        [Required]
        public decimal InAppPurchaseUserAmount { get; set; }
    }
}