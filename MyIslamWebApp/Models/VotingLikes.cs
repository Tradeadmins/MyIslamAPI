using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class VotingLikes : BaseModel
    {
        /// <summary>
        /// Id of the Voting Likes Table
        /// </summary>
        [Key]
        public int VotingLikesId { get; set; }
        /// <summary>
        /// Id of the Voting Table
        /// </summary>
        [ForeignKey("Voting")]
        public int VotingId { get; set; }
        public Voting Voting { get; set; }
        /// <summary>
        /// Id of the Voting Category Table
        /// </summary>
        [ForeignKey("VotingCategory")]
        public int VotingCategoryId { get; set; }
        public VotingOption VotingCategory { get; set; }
        /// <summary>
        ///Voting yUserId
        /// </summary>
        public string VotingByUserId { get; set; }
        
    }
}