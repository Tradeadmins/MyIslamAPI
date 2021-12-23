using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.DonationCategorys
{
    /// <summary>
    /// CRUD operations for DonationCategory table using Generic Repository Pattern
    /// </summary>
    public class DonationCategoryRepository : RepositoryBase<DonationCategory>, IDonationCategoryRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public DonationCategoryRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public DonationCategoryRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods        
        /// <summary>
        /// To add a new record in DonationCategory table
        /// </summary>
        /// <param name="donationCategory">Instance of DonationCategory class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddDonationCategory(DonationCategory donationCategory)
        {
            try
            {
                if (donationCategory == null)
                    return false;

                _dbContext.DonationCategorys.Add(donationCategory);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in DonationCategory table
        /// </summary>
        /// <param name="donationCategory">The instance of DonationCategory class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateDonationCategory(DonationCategory donationCategory)
        {
            try
            {
                if (donationCategory == null)
                    return false;

                _dbContext.Entry(donationCategory).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the DonationCategory table
        /// </summary>
        /// <param name="donationCategory">The instance of DonationCategory class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDonationCategory(DonationCategory donationCategory)
        {
            try
            {
                if (donationCategory == null)
                    return false;

                _dbContext.Entry(donationCategory).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using DonationCategory ID of the DonationCategory table
        /// </summary>
        /// <param name="donationCategoryId">Primary Key of the row (DonationCategory ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteDonationCategoryById(int donationCategoryId)
        {
            try
            {
                if (int.Equals(donationCategoryId, 0))
                    return false;

                DonationCategory record = _dbContext.DonationCategorys.Find(donationCategoryId);

                if (record == null)
                    return false;

                _dbContext.DonationCategorys.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using DonationCategory ID from DonationCategory table
        /// </summary>
        /// <param name="donationCategoryId">Primary Key of the row (DonationCategory ID)</param>
        /// <returns>Returns a DonationCategory row matching the passing ID</returns>
        public DonationCategory GetDonationCategoryById(int donationCategoryId)
        {
            try
            {
                if (Equals(donationCategoryId, 0))
                    return null;

                return _dbContext.DonationCategorys.AsNoTracking().Where(x => x.DonationCategoryId == donationCategoryId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from DonationCategory table
        /// </summary>
        /// <returns>An IQuerable List of DonationCategory</returns>
        public IEnumerable<DonationCategory> GetAllDonationCategorys()
        {
            try
            {
                return _dbContext.DonationCategorys.Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}