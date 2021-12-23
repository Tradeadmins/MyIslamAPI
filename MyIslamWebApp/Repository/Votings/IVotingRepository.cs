using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.Votings
{
    public interface IVotingRepository : IRepository<Voting>
    {
        bool AddVoting(Voting voting);
        bool DeleteVoting(Voting voting);
        bool DeleteVotingById(int votingId);
        bool UpdateVoting(Voting voting);
        Voting GetVotingById(int votingId);
        IEnumerable<Voting> GetAllVotings();
    }
}