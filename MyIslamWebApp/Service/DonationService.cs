using System;
using System.Collections.Generic;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;
using MyIslamWebApp.DataTransferObjects.Donation;

namespace MyIslamWebApp.Service
{
    public class DonationService : IDonationService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DonationService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public DonationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Donation Methods
        public bool AddDonation(DonationDTO donation, string userId)
        {
            try
            {
                var result = false;
                if (donation == null)
                    return result;

                var addDonation = new Donation();
                addDonation.DonationId = donation.DonationId;
                addDonation.DonationCategoryId = donation.DonationCategoryId;
                addDonation.DonationAmount = donation.DonationAmount;
                addDonation.DonationLocalAmount = donation.DonationLocalAmount;
                addDonation.DonationLocalCurrencyType = donation.DonationLocalCurrencyType;

                //Default Values
                addDonation.IsActive = true;
                addDonation.CreatedBy = userId;
                addDonation.CreatedOn = DateTime.Now;
                addDonation.UpdatedBy = userId;
                addDonation.UpdatedOn = DateTime.Now;
                result = _unitOfWork.donationRepository.AddDonation(addDonation);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateDonation(DonationDTO donation, string userId)
        {
            try
            {
                var result = false;
                if (donation == null)
                    return result;

                var _donations = _unitOfWork.donationRepository.GetDonationById(donation.DonationId);

                if (_donations == null)
                    return result;

                var updateDonation = new Donation();
                updateDonation.DonationId = donation.DonationId;
                updateDonation.DonationCategoryId = donation.DonationCategoryId;
                updateDonation.DonationAmount = donation.DonationAmount;
                updateDonation.DonationLocalAmount = donation.DonationLocalAmount;
                updateDonation.DonationLocalCurrencyType = donation.DonationLocalCurrencyType;

                result = _unitOfWork.donationRepository.UpdateDonation(updateDonation);

                ////Default Values
                updateDonation.IsActive = _donations.IsActive;
                updateDonation.CreatedBy = _donations.CreatedBy;
                updateDonation.CreatedOn = _donations.CreatedOn;
                updateDonation.UpdatedBy = userId;
                updateDonation.UpdatedOn = DateTime.Now;

                result = _unitOfWork.donationRepository.UpdateDonation(updateDonation);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDonation(DonationDTO donation, string userId)
        {
            try
            {
                var result = false;
                if (donation == null)
                    return result;

                var deleteDonation = new Donation();
                deleteDonation.DonationId = donation.DonationId;
                deleteDonation.DonationCategoryId = donation.DonationCategoryId;
                deleteDonation.DonationAmount = donation.DonationAmount;
                deleteDonation.DonationLocalAmount = donation.DonationLocalAmount;
                deleteDonation.DonationLocalCurrencyType = donation.DonationLocalCurrencyType;

                ////Default Values
                deleteDonation.IsActive = false;
                deleteDonation.UpdatedBy = userId;
                deleteDonation.UpdatedOn = DateTime.Now;

                result = _unitOfWork.donationRepository.DeleteDonation(deleteDonation);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDonationById(int donationID, string userId)
        {
            try
            {
                var result = false;
                if (donationID == 0)
                    return result;

                var donation = _unitOfWork.donationRepository.GetDonationById(donationID);

                if (donation == null)
                    return result;

                var deletedonation = new Donation();
                deletedonation = donation;
                ////Default Values
                deletedonation.IsActive = false;
                deletedonation.UpdatedBy = userId;
                deletedonation.UpdatedOn = DateTime.Now;

                result = _unitOfWork.donationRepository.DeleteDonation(deletedonation);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DonationDTO GetDonationById(int donationID)
        {
            try
            {
                DonationDTO donationResponse = new DonationDTO();
                var donation = _unitOfWork.donationRepository.GetDonationById(donationID);
                if (donation == null)
                    return null;

                if (donation.IsActive)
                {
                    donationResponse.DonationId = donation.DonationId;
                    donationResponse.DonationCategoryId = donation.DonationCategoryId;
                    donationResponse.DonationAmount = donation.DonationAmount;
                    donationResponse.DonationLocalAmount = donation.DonationLocalAmount;
                    donationResponse.DonationLocalCurrencyType = donation.DonationLocalCurrencyType;
                }
                return donationResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetAllDonationAmounts()
        {
            try
            {
                var allDonations = _unitOfWork.donationRepository.GetAllDonationAmounts();
                return allDonations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetAllDonationByUserId(string userId)
        {
            try
            {
                var allDonations = _unitOfWork.donationRepository.GetAllDonationByUserId(userId);
                return allDonations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}