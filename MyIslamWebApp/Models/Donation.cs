using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class Donation : BaseModel
    {
        /// <summary>
        /// Id of the Donation
        /// </summary>
        [Key]
        public int DonationId { get; set; }
        /// <summary>
        /// Donation Category
        /// </summary>
        [ForeignKey("DonationCategory")]
        public int DonationCategoryId { get; set; }
        public DonationCategory DonationCategory { get; set; }
        /// <summary>
        /// DonationInApp's Total Amount deducted from user
        /// </summary>
        [Required]
        public decimal DonationAmount { get; set; }
        /// <summary>
        /// DonationInApp's Total Amount in local currecny
        /// </summary>
        [Required]
        public decimal DonationLocalAmount { get; set; }
        /// <summary>
        /// DonationInApp's Amount currency type
        /// </summary>
        [Required]
        public string DonationLocalCurrencyType { get; set; }
    }
}