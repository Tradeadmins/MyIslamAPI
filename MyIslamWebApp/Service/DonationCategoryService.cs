using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.DonationCategory;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class DonationCategoryService : IDonationCategoryService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DonationCategoryService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public DonationCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region DonationCategory Methods
        public bool AddDonationCategory(DonationCategoryDTO donationCategory, string userId)
        {
            try
            {
                var result = false;

                if (donationCategory == null)
                    return result;

                var addDonationCategory = new DonationCategory();
                addDonationCategory.DonationCategoryId = donationCategory.DonationCategoryId;
                addDonationCategory.DonationCategoryName = donationCategory.DonationCategoryName;

                //Default Values
                addDonationCategory.IsActive = true;
                addDonationCategory.CreatedBy = userId;
                addDonationCategory.CreatedOn = DateTime.Now;
                addDonationCategory.UpdatedBy = userId;
                addDonationCategory.UpdatedOn = DateTime.Now;

                result = _unitOfWork.donationCategoryRepository.AddDonationCategory(addDonationCategory);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateDonationCategory(DonationCategoryDTO donationCategory, string userId)
        {
            try
            {
                var result = false;
                if (donationCategory == null)
                    return result;

                var _donationCategory = _unitOfWork.donationCategoryRepository.GetDonationCategoryById(donationCategory.DonationCategoryId);

                if (_donationCategory == null)
                    return result;

                var updateDonationCategory = new DonationCategory();
                updateDonationCategory.DonationCategoryId = donationCategory.DonationCategoryId;
                updateDonationCategory.DonationCategoryName = donationCategory.DonationCategoryName;

                ////Default Values
                updateDonationCategory.IsActive = _donationCategory.IsActive;
                updateDonationCategory.CreatedBy = _donationCategory.CreatedBy;
                updateDonationCategory.CreatedOn = _donationCategory.CreatedOn;
                updateDonationCategory.UpdatedBy = userId;
                updateDonationCategory.UpdatedOn = DateTime.Now;

                result = _unitOfWork.donationCategoryRepository.UpdateDonationCategory(updateDonationCategory);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDonationCategory(DonationCategoryDTO donationCategory, string userId)
        {
            try
            {
                var result = false;
                if (donationCategory == null)
                    return result;

                var deleteDonationCategory = new DonationCategory();
                deleteDonationCategory.DonationCategoryId = donationCategory.DonationCategoryId;
                deleteDonationCategory.DonationCategoryName = donationCategory.DonationCategoryName;

                deleteDonationCategory.IsActive = false;
                deleteDonationCategory.UpdatedBy = userId;
                deleteDonationCategory.UpdatedOn = DateTime.Now;

                result = _unitOfWork.donationCategoryRepository.DeleteDonationCategory(deleteDonationCategory);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDonationCategoryById(int donationCategoryID, string userId)
        {
            try
            {
                var result = false;
                if (donationCategoryID == 0)
                    return result;

                var donationCategory = _unitOfWork.donationCategoryRepository.GetDonationCategoryById(donationCategoryID);

                if (donationCategory == null)
                    return result;

                var deleteDonationCategory = new DonationCategory();
                deleteDonationCategory = donationCategory;

                ////Default Values
                deleteDonationCategory.IsActive = false;
                deleteDonationCategory.UpdatedBy = userId;
                deleteDonationCategory.UpdatedOn = DateTime.Now;

                result = _unitOfWork.donationCategoryRepository.DeleteDonationCategory(deleteDonationCategory);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DonationCategoryDTO GetDonationCategoryById(int donationCategoryID)
        {
            try
            {
                DonationCategoryDTO donationCategoryResponse = new DonationCategoryDTO();
                var donationCategory = _unitOfWork.donationCategoryRepository.GetDonationCategoryById(donationCategoryID);
                if (donationCategory == null)
                    return null;

                if (donationCategory.IsActive)
                {
                    donationCategoryResponse.DonationCategoryId = donationCategory.DonationCategoryId;
                    donationCategoryResponse.DonationCategoryName = donationCategory.DonationCategoryName;
                }
                return donationCategoryResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DonationCategoryDTO> GetAllDonationCategory()
        {
            try
            {
                var response = new List<DonationCategoryDTO>();                
                DonationCategoryDTO donationCategoryResponse;
                var allDonationCategorys = _unitOfWork.donationCategoryRepository.GetAllDonationCategorys();

                if (allDonationCategorys == null)
                    return response;

                foreach (var donationCategory in allDonationCategorys)
                {
                    if (donationCategory.IsActive)
                    {
                        donationCategoryResponse = new DonationCategoryDTO();
                        donationCategoryResponse.DonationCategoryId = donationCategory.DonationCategoryId;
                        donationCategoryResponse.DonationCategoryName = donationCategory.DonationCategoryName;
                        response.Add(donationCategoryResponse);
                    }
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}