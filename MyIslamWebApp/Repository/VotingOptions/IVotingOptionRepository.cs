using System;
using System.Collections.Generic;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Repository.VotingOptions
{

    public interface IVotingOptionRepository : IRepository<VotingOption>
    {
        IEnumerable<VotingOption> GetAllVotingOptions();
        VotingOption GetVotingOptionById(int dailyQuoteId);     
        bool AddVotingOption(VotingOption dailyQuote);
        bool DeleteVotingOption(VotingOption dailyQuote);
        bool DeleteVotingOptionById(int dailyQuoteId);
        bool UpdateVotingOption(VotingOption dailyQuote);
    }
}