using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Repository.VotingOptions
{
    /// <summary>
    /// CRUD operations for VotingOption table using Generic Repository Pattern
    /// </summary>
    public class VotingOptionRepository : RepositoryBase<VotingOption>, IVotingOptionRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public VotingOptionRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public VotingOptionRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in Daily Quote table
        /// </summary>
        /// <param name="votingOption">Instance of VotingOption class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddVotingOption(VotingOption votingOption)
        {
            try
            {
                if (votingOption == null)
                    return false;

                _dbContext.VotingOptions.Add(votingOption);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the VotingOption table
        /// </summary>
        /// <param name="votingOption">The instance of VotingOption class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteVotingOption(VotingOption votingOption)
        {
            try
            {
                if (votingOption == null)
                    return false;

                //var deleteVotingOption = _dbContext.VotingOptions.FirstOrDefault(emp => emp.VotingOptionId == votingOption.VotingOptionId);
                //_dbContext.VotingOptions.Remove(deleteVotingOption);
                _dbContext.Entry(votingOption).State = EntityState.Modified;

                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using VotingOption ID of the VotingOption table
        /// </summary>
        /// <param name="votingOptionId">Primary Key of the row (VotingOption ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteVotingOptionById(int votingOptionId)
        {
            try
            {
                if (int.Equals(votingOptionId, 0))
                    return false;

                VotingOption record = _dbContext.VotingOptions.Find(votingOptionId);

                if (record == null)
                    return false;

                _dbContext.VotingOptions.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from VotingOption table
        /// </summary>
        /// <returns>An IQuerable List of VotingOption</returns>
        public IEnumerable<VotingOption> GetAllVotingOptions()
        {
            try
            {
                return _dbContext.VotingOptions.Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using VotingOption ID from VotingOption table
        /// </summary>
        /// <param name="votingOptionId">Primary Key of the row (VotingOption ID)</param>
        /// <returns>Returns a VotingOption row matching the passing ID</returns>
        public VotingOption GetVotingOptionById(int votingOptionId)
        {
            try
            {
                if (Equals(votingOptionId, 0))
                    return null;

                return _dbContext.VotingOptions.AsNoTracking().Where(x => x.VotingOptionId == votingOptionId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in VotingOption table
        /// </summary>
        /// <param name="votingOption">The instance of VotingOption class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateVotingOption(VotingOption votingOption)
        {
            try
            {
                if (votingOption == null)
                    return false;

                _dbContext.Entry(votingOption).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}