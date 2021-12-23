using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.Votings
{
    /// <summary>
    /// CRUD operations for Voting table using Generic Repository Pattern
    /// </summary>
    public class VotingRepository : RepositoryBase<Voting>, IVotingRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public VotingRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public VotingRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in Voting table
        /// </summary>
        /// <param name="voting">Instance of Voting class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddVoting(Voting voting)
        {
            try
            {
                if (voting == null)
                    return false;

                _dbContext.Votings.Add(voting);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in Voting table
        /// </summary>
        /// <param name="voting">The instance of Voting class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateVoting(Voting voting)
        {
            try
            {
                if (voting == null)
                    return false;

                _dbContext.Entry(voting).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the Voting table
        /// </summary>
        /// <param name="voting">The instance of Voting class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteVoting(Voting voting)
        {
            try
            {
                if (voting == null)
                    return false;

                _dbContext.Entry(voting).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using Voting ID of the Voting table
        /// </summary>
        /// <param name="votingId">Primary Key of the row (Voting ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteVotingById(int votingId)
        {
            try
            {
                if (int.Equals(votingId, 0))
                    return false;

                Voting record = _dbContext.Votings.Find(votingId);

                if (record == null)
                    return false;

                _dbContext.Votings.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Voting ID from Voting table
        /// </summary>
        /// <param name="votingId">Primary Key of the row (Voting ID)</param>
        /// <returns>Returns a Voting row matching the passing ID</returns>
        public Voting GetVotingById(int votingId)
        {
            try
            {
                if (Equals(votingId, 0))
                    return null;

                return _dbContext.Votings.AsNoTracking().Where(x => x.VotingId == votingId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from Voting table
        /// </summary>
        /// <returns>An IQuerable List of Voting</returns>
        public IEnumerable<Voting> GetAllVotings()
        {
            try
            {
                return _dbContext.Votings.Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}