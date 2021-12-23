using System;
using System.Collections.Generic;
using MyIslamWebApp.DataTransferObjects.VotionOption;

namespace MyIslamWebApp.Service
{
    public interface IVotingOptionService
    {
        //VotingOption CRUD Operations
        IEnumerable<VotingOptionDTO> GetAllVotingOptions();
        VotingOptionDTO GetVotingOptionById(int votingOptionOptionId);        
        bool AddVotingOption(VotingOptionDTO votingOptionOption, string userId);
        bool DeleteVotingOptionById(int votingOptionOptionID, string userId);
        bool UpdateVotingOption(VotingOptionDTO votingOption, string userId);
        bool DeleteVotingOption(VotingOptionDTO votingOptionOption, string userId);
    }
}