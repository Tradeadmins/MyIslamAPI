using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.Voting;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;

namespace MyIslamWebApp.Service
{
    public class VotingService : IVotingService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public VotingService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public VotingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Voting Methods
        public bool AddVoting(VotingDTO voting, string userId)
        {
            try
            {
                var result = false;

                if (voting == null)
                    return result;

                var addVoting = new Voting();
                addVoting.VotingId = voting.VotingId;
                addVoting.VotingTitle = voting.VotingTitle;
                addVoting.VotingStartDate = voting.VotingStartDate;
                addVoting.VotingEndDate = voting.VotingEndDate;
                addVoting.VotingDescription = voting.VotingDescription;

                //Default Values
                addVoting.IsActive = true;
                addVoting.CreatedBy = userId;
                addVoting.CreatedOn = DateTime.Now;
                addVoting.UpdatedBy = userId;
                addVoting.UpdatedOn = DateTime.Now;

                result = _unitOfWork.votingRepository.AddVoting(addVoting);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateVoting(VotingDTO voting, string userId)
        {
            try
            {
                var result = false;
                if (voting == null)
                    return result;

                var _voting = _unitOfWork.votingRepository.GetVotingById(voting.VotingId);

                if (_voting == null)
                    return result;

                var updateVoting = new Voting();
                updateVoting.VotingId = voting.VotingId;
                updateVoting.VotingTitle = voting.VotingTitle;
                updateVoting.VotingStartDate = voting.VotingStartDate;
                updateVoting.VotingEndDate = voting.VotingEndDate;
                updateVoting.VotingDescription = voting.VotingDescription;
                ////Default Values
                updateVoting.IsActive = _voting.IsActive;
                updateVoting.CreatedBy = _voting.CreatedBy;
                updateVoting.CreatedOn = _voting.CreatedOn;
                updateVoting.UpdatedBy = userId;
                updateVoting.UpdatedOn = DateTime.Now;

                result = _unitOfWork.votingRepository.UpdateVoting(updateVoting);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteVoting(VotingDTO voting, string userId)
        {
            try
            {
                var result = false;
                if (voting == null)
                    return result;

                var deleteVoting = new Voting();             
                deleteVoting.VotingId = voting.VotingId;
                deleteVoting.VotingTitle = voting.VotingTitle;
                deleteVoting.VotingStartDate = voting.VotingStartDate;
                deleteVoting.VotingEndDate = voting.VotingEndDate;
                deleteVoting.VotingDescription = voting.VotingDescription;

                //DefaultValues
                deleteVoting.IsActive = false;
                deleteVoting.UpdatedBy = userId;
                deleteVoting.UpdatedOn = DateTime.Now;

                result = _unitOfWork.votingRepository.DeleteVoting(deleteVoting);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteVotingById(int votingID, string userId)
        {
            try
            {
                var result = false;
                if (votingID == 0)
                    return result;

                var voting = _unitOfWork.votingRepository.GetVotingById(votingID);

                if (voting == null)
                    return result;

                var deleteVoting = new Voting();
                deleteVoting = voting;

                ////Default Values
                deleteVoting.IsActive = false;
                deleteVoting.UpdatedBy = userId;
                deleteVoting.UpdatedOn = DateTime.Now;

                result = _unitOfWork.votingRepository.DeleteVoting(deleteVoting);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VotingDTO GetVotingById(int votingID)
        {
            try
            {
                VotingDTO votingResponse = new VotingDTO();
                var voting = _unitOfWork.votingRepository.GetVotingById(votingID);
                if (voting == null)
                    return null;

                if (voting.IsActive)
                {
                    votingResponse.VotingId = voting.VotingId;
                    votingResponse.VotingTitle = voting.VotingTitle;
                    votingResponse.VotingStartDate = voting.VotingStartDate;
                    votingResponse.VotingEndDate = voting.VotingEndDate;
                    votingResponse.VotingDescription = voting.VotingDescription;

                }
                return votingResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<VotingDTO> GetAllVotings()
        {
            try
            {
                var response = new List<VotingDTO>();
                VotingDTO votingResponse;
                var allVotings = _unitOfWork.votingRepository.GetAllVotings();
                if (allVotings == null)
                    return response;

                foreach (var voting in allVotings)
                {
                    if (voting.IsActive)    
                    {
                        votingResponse = new VotingDTO();
                        votingResponse.VotingId = voting.VotingId;
                        votingResponse.VotingTitle = voting.VotingTitle;
                        votingResponse.VotingStartDate = voting.VotingStartDate;
                        votingResponse.VotingEndDate = voting.VotingEndDate;
                        votingResponse.VotingDescription = voting.VotingDescription;

                        response.Add(votingResponse);
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