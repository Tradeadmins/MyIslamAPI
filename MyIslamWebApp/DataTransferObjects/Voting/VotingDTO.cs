using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.Voting
{
    public class VotingDTO
    {        
        public int VotingId { get; set; }    
        public string VotingTitle { get; set; }     
        public DateTime VotingStartDate { get; set; }      
        public DateTime VotingEndDate { get; set; }     
        public string VotingDescription { get; set; }
    }
}