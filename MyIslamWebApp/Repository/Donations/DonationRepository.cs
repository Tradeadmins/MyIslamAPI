using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Donation;

namespace MyIslamWebApp.Repository.Donations
{
    /// <summary>
    /// CRUD operations for Donation table using Generic Repository Pattern
    /// </summary>
    public class DonationRepository : RepositoryBase<Donation>, IDonationRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public DonationRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public DonationRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in Daily Quote table
        /// </summary>
        /// <param name="donation">Instance of Donation class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddDonation(Donation donation)
        {
            try
            {
                if (donation == null)
                    return false;

                _dbContext.Donations.Add(donation);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in Donation table
        /// </summary>
        /// <param name="donation">The instance of Donation class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateDonation(Donation donation)
        {
            try
            {
                if (donation == null)
                    return false;

                _dbContext.Entry(donation).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the Donation table
        /// </summary>
        /// <param name="donation">The instance of Donation class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDonation(Donation donation)
        {
            try
            {
                if (donation == null)
                    return false;

                //var deleteDonation = _dbContext.Donations.FirstOrDefault(emp => emp.DonationId == donation.DonationId);
                //_dbContext.Donations.Remove(deleteDonation);
                _dbContext.Entry(donation).State = EntityState.Modified;

                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using Donation ID of the Donation table
        /// </summary>
        /// <param name="donationId">Primary Key of the row (Donation ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDonationById(int donationId)
        {
            try
            {
                if (int.Equals(donationId, 0))
                    return false;

                Donation record = _dbContext.Donations.Find(donationId);

                if (record == null)
                    return false;

                _dbContext.Donations.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Donation ID from Donation table
        /// </summary>
        /// <param name="donationId">Primary Key of the row (Donation ID)</param>
        /// <returns>Returns a Donation row matching the passing ID</returns>
        public Donation GetDonationById(int donationId)
        {
            try
            {
                if (Equals(donationId, 0))
                    return null;

                return _dbContext.Donations.AsNoTracking().Where(x => x.DonationId == donationId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from Donation table
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public decimal GetAllDonationAmounts()
        {
            try
            {
                return _dbContext.Donations.Where(x => x.IsActive == true).Sum(x=>x.DonationAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all record in Donation table
        /// </summary>
        /// <param name="userId">The instance of Donation class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public decimal GetAllDonationByUserId(string userId)
        {
            try
            {                
                return _dbContext.Donations.Where(x => x.IsActive == true && x.CreatedBy == userId).Sum(x => x.DonationLocalAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}