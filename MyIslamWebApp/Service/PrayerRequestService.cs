using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class PrayerRequestService : IPrayerRequestService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public PrayerRequestService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public PrayerRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region PrayerRequest Methods

        public bool AddPrayerRequest(PrayerRequestDTO prayerRequest, string userId)
        {
            try
            {
                var result = false;

                if (prayerRequest == null)
                    return result;

                var addPrayerRequest = new PrayerRequest();
                addPrayerRequest.PrayerRequestId = prayerRequest.PrayerRequestId;
                addPrayerRequest.PrayerRequestText = prayerRequest.PrayerRequestText;

                //Default Values
                addPrayerRequest.IsActive = true;
                addPrayerRequest.CreatedBy = userId;
                addPrayerRequest.CreatedOn = DateTime.Now;
                addPrayerRequest.UpdatedBy = userId;
                addPrayerRequest.UpdatedOn = DateTime.Now;

                result = _unitOfWork.prayerRequestRepository.AddPrayerRequest(addPrayerRequest);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePrayerRequest(PrayerRequestDTO prayerRequest, string userId)
        {
            try
            {
                var result = false;
                if (prayerRequest == null)
                    return result;

                var deletePrayerRequest = new PrayerRequest();
                deletePrayerRequest.PrayerRequestId = prayerRequest.PrayerRequestId;
                deletePrayerRequest.PrayerRequestText = prayerRequest.PrayerRequestText;

                result = _unitOfWork.prayerRequestRepository.DeletePrayerRequest(deletePrayerRequest);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePrayerRequestById(int prayerRequestID, string userId)
        {
            try
            {
                var result = false;
                if (prayerRequestID == 0)
                    return result;

                var _prayerRequest = _unitOfWork.prayerRequestRepository.GetPrayerRequestById(prayerRequestID);

                if (_prayerRequest == null)
                    return result;

                var prayerRequest = new PrayerRequest();
                prayerRequest = _prayerRequest;

                ////Default Values
                prayerRequest.IsActive = false;
                prayerRequest.UpdatedBy = userId;
                prayerRequest.UpdatedOn = DateTime.Now;

                result = _unitOfWork.prayerRequestRepository.DeletePrayerRequest(prayerRequest);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<PrayerRequestListDTO> GetAllPrayerRequests(int pageIndex, int pageSize, string userId)
        {
            try
            {
                var response = new ResultDTO<PrayerRequestListDTO>();
                List<PrayerRequestListDTO> prayerRequestDTOList = new List<PrayerRequestListDTO>();
                PrayerRequestListDTO prayerRequestResponse;

                var allPrayerRequests = _unitOfWork.prayerRequestRepository.GetAllPrayerRequests(pageIndex, pageSize, userId);

                if (allPrayerRequests == null)
                    return response;

                foreach (var prayerRequest in allPrayerRequests.Response)
                {
                    prayerRequestResponse = new PrayerRequestListDTO();
                    prayerRequestResponse.PrayerRequestId = prayerRequest.PrayerRequestId;
                    prayerRequestResponse.PrayerRequestText = prayerRequest.PrayerRequestText;
                    prayerRequestResponse.PrayerRequestTotalDuaCount = prayerRequest.PrayerRequestTotalDuaCount;
                    prayerRequestResponse.PrayerRequestIsLiked = prayerRequest.PrayerRequestIsLiked;
                    prayerRequestDTOList.Add(prayerRequestResponse);
                }
                response.Response = prayerRequestDTOList;
                response.TotalCount = allPrayerRequests.TotalCount;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrayerRequestDTO GetPrayerRequestById(int prayerRequestID)
        {
            try
            {
                PrayerRequestDTO prayerRequestResponse = new PrayerRequestDTO();
                var prayerRequest = _unitOfWork.prayerRequestRepository.GetPrayerRequestById(prayerRequestID);
                if (prayerRequest == null)
                    return null;

                if (prayerRequest.IsActive)
                {
                    prayerRequestResponse.PrayerRequestId = prayerRequest.PrayerRequestId;
                    prayerRequestResponse.PrayerRequestText = prayerRequest.PrayerRequestText;
                }
                return prayerRequestResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePrayerRequest(PrayerRequestDTO prayerRequest, string userId)
        {
            try
            {
                var result = false;
                if (prayerRequest == null)
                    return result;

                var _prayerRequest = _unitOfWork.prayerRequestRepository.GetPrayerRequestById(prayerRequest.PrayerRequestId);

                if (_prayerRequest == null)
                    return result;

                var updatePrayerRequest = new PrayerRequest();
                updatePrayerRequest.PrayerRequestId = prayerRequest.PrayerRequestId;
                updatePrayerRequest.PrayerRequestText = prayerRequest.PrayerRequestText;

                result = _unitOfWork.prayerRequestRepository.UpdatePrayerRequest(updatePrayerRequest);

                ////Default Values
                updatePrayerRequest.IsActive = _prayerRequest.IsActive;
                updatePrayerRequest.CreatedBy = _prayerRequest.CreatedBy;
                updatePrayerRequest.CreatedOn = _prayerRequest.CreatedOn;
                updatePrayerRequest.UpdatedBy = userId;
                updatePrayerRequest.UpdatedOn = DateTime.Now;

                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Result<PrayerRequestListDTO> GetAllPrayerRequestByUserId(string prayerRequestUserId)
        {
            try
            {
                var allPrayerRequests = _unitOfWork.prayerRequestRepository.GetAllPrayerRequestByUserId(prayerRequestUserId);
                return allPrayerRequests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}