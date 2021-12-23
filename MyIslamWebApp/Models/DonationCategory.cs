using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class DonationCategory : BaseModel
    {
        /// <summary>
        /// Id of the DonationCategory
        /// </summary>
        [Key]
        public int DonationCategoryId { get; set; }
        /// <summary>
        /// Donation Category Name
        /// </summary>
        public string DonationCategoryName { get; set; }
    }
}