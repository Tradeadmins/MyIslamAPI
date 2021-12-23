using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.VotionOption
{
    public class VotingOptionDTO
    {
       
        public int VotingOptionId { get; set; }
        public int VotingId { get; set; }
        public int DonationCategoryId { get; set; }
    
    }
}