using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class Voting : BaseModel
    {
        /// <summary>
        /// Id of the Voting
        /// </summary>
        [Key]
        public int VotingId { get; set; }
        /// <summary>
        /// Voting's Title
        /// </summary>
        [Required]
        public string VotingTitle { get; set; }
        /// <summary>
        /// Voting's Star tDate
        /// </summary>
        [Required]
        public DateTime VotingStartDate { get; set; }
        /// <summary>
        /// Voting's End Date
        /// </summary>
        [Required]
        public DateTime VotingEndDate { get; set; }
        /// <summary>
        /// Voting's Description
        /// </summary>      
        public string VotingDescription { get; set; }
    }
}