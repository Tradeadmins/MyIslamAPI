using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class VotingOption : BaseModel
    {
        /// <summary>
        /// Id of the Voting Option Table
        /// </summary>
        [Key]
        public int VotingOptionId { get; set; }
        /// <summary>
        /// Id of the Voting Table
        /// </summary>
        [ForeignKey("Voting")]
        public int VotingId { get; set; }
        public Voting Voting { get; set; }
        /// <summary>
        /// Id of the Donation Category Table
        /// </summary>
        [ForeignKey("DonationCategory")]
        public int DonationCategoryId { get; set; }
        public DonationCategory DonationCategory { get; set; }
    }
}