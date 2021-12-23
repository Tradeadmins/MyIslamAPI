using System;
using System.Collections.Generic;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.VotionOption;

namespace MyIslamWebApp.Service
{
    public class VotingOptionService : IVotingOptionService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public VotingOptionService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public VotingOptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region VotingOption Methods
        public bool AddVotingOption(VotingOptionDTO votingOption, string userId)
        {
            try
            {
                var result = false;
                if (votingOption == null)
                    return result;

                var addVotingOption = new VotingOption();
                addVotingOption.VotingOptionId = votingOption.VotingOptionId;
                addVotingOption.VotingId = votingOption.VotingId;
                addVotingOption.DonationCategoryId = votingOption.DonationCategoryId;

                //Default Values
                addVotingOption.IsActive = true;
                addVotingOption.CreatedBy = userId;
                addVotingOption.CreatedOn = DateTime.Now;
                addVotingOption.UpdatedBy = userId;
                addVotingOption.UpdatedOn = DateTime.Now;
                result = _unitOfWork.votingOptionRepository.AddVotingOption(addVotingOption);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateVotingOption(VotingOptionDTO votingOption, string userId)
        {
            try
            {
                var result = false;
                if (votingOption == null)
                    return result;

                var _votingOptions = _unitOfWork.votingOptionRepository.GetVotingOptionById(votingOption.VotingOptionId);

                if (_votingOptions == null)
                    return result;

                var updateVotingOption = new VotingOption();
                updateVotingOption.VotingOptionId = votingOption.VotingOptionId;
              
                result = _unitOfWork.votingOptionRepository.UpdateVotingOption(updateVotingOption);

                ////Default Values
                updateVotingOption.IsActive = _votingOptions.IsActive;
                updateVotingOption.CreatedBy = _votingOptions.CreatedBy;
                updateVotingOption.CreatedOn = _votingOptions.CreatedOn;
                updateVotingOption.UpdatedBy = userId;
                updateVotingOption.UpdatedOn = DateTime.Now;

                result = _unitOfWork.votingOptionRepository.UpdateVotingOption(updateVotingOption);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteVotingOption(VotingOptionDTO votingOption, string userId)
        {
            try
            {
                var result = false;
                if (votingOption == null)
                    return result;

                var deleteVotingOption = new VotingOption();
                deleteVotingOption.VotingOptionId = votingOption.VotingOptionId;
               

                ////Default Values
                deleteVotingOption.IsActive = false;
                deleteVotingOption.UpdatedBy = userId;
                deleteVotingOption.UpdatedOn = DateTime.Now;

                result = _unitOfWork.votingOptionRepository.DeleteVotingOption(deleteVotingOption);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteVotingOptionById(int votingOptionID, string userId)
        {
            try
            {
                var result = false;
                if (votingOptionID == 0)
                    return result;

                var votingOption = _unitOfWork.votingOptionRepository.GetVotingOptionById(votingOptionID);

                if (votingOption == null)
                    return result;

                var deletevotingOption = new VotingOption();
                deletevotingOption = votingOption;
                ////Default Values
                deletevotingOption.IsActive = false;
                deletevotingOption.UpdatedBy = userId;
                deletevotingOption.UpdatedOn = DateTime.Now;

                result = _unitOfWork.votingOptionRepository.DeleteVotingOption(deletevotingOption);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VotingOptionDTO> GetAllVotingOptions()
        {
            try
            {
                var response = new List<VotingOptionDTO>();
                VotingOptionDTO votingOptionResponse;
                var allVotingOptions = _unitOfWork.votingOptionRepository.GetAllVotingOptions();
                if (allVotingOptions == null)
                    return response;
                foreach (var votingOption in allVotingOptions)
                {
                    if (votingOption.IsActive)
                    {
                        votingOptionResponse = new VotingOptionDTO();
                        votingOptionResponse.VotingOptionId = votingOption.VotingOptionId;
                       
                        response.Add(votingOptionResponse);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VotingOptionDTO GetVotingOptionById(int votingOptionID)
        {
            try
            {
                VotingOptionDTO votingOptionResponse = new VotingOptionDTO();
                var votingOption = _unitOfWork.votingOptionRepository.GetVotingOptionById(votingOptionID);
                if (votingOption == null)
                    return null;

                if (votingOption.IsActive)
                {
                    votingOptionResponse.VotingOptionId = votingOption.VotingOptionId;
                   
                }
                return votingOptionResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}