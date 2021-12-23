using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.Voting;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Service
{
    public interface IVotingService
    {
        //Voting CRUD Operations
        IEnumerable<VotingDTO> GetAllVotings();
        VotingDTO GetVotingById(int dailyQuoteId);
        bool DeleteVoting(VotingDTO dua, string userId);
        bool AddVoting(VotingDTO dailyQuote, string userId);
        bool DeleteVotingById(int dailyQuoteId, string userId);
        bool UpdateVoting(VotingDTO dailyQuote, string userId);
    }
}