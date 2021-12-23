using MyIslamWebApp.DataTransferObjects.UmrahGuideComplete;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyIslamWebApp.Service
{
    public class UmrahGuideCompleteService : IUmrahGuideCompleteService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public UmrahGuideCompleteService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public UmrahGuideCompleteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region UmrahGuideComplete Methods
        public IEnumerable<UmrahGuideCompleteDTO> GetAllUmrahGuideComplete()
        {
            try
            {
                var response = new List<UmrahGuideCompleteDTO>();
                UmrahGuideCompleteDTO UmrahGuideCompleteResponse;
                var allUmrahGuideComplete = _unitOfWork.umrahGuideCompleteRepository.GetAllUmrahGuideComplete();
                if (allUmrahGuideComplete == null)
                    return response;
                foreach (var UmrahGuideComplete in allUmrahGuideComplete)
                {
                    if (UmrahGuideComplete.IsActive)
                    {
                        UmrahGuideCompleteResponse = new UmrahGuideCompleteDTO();
                        UmrahGuideCompleteResponse.UmrahGuideId = UmrahGuideComplete.UmrahGuideId;
                        UmrahGuideCompleteResponse.UmrahGuideCompleteId = UmrahGuideComplete.UmrahGuideCompleteId;
                        UmrahGuideCompleteResponse.UmrahGuideCompleteByUserId = UmrahGuideComplete.UmrahGuideCompleteByUserId;
                        response.Add(UmrahGuideCompleteResponse);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUmrahGuideComplete(int umrahGuideCompleteId)
        {
            try
            {
                var result = false;
                if (umrahGuideCompleteId == 0)
                    return result;

                result = _unitOfWork.umrahGuideCompleteRepository.DeleteUmrahGuideComplete(umrahGuideCompleteId);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddUmrahGuideComplete(UmrahGuideCompleteDTO umrahGuideComplete, string userId)
        {
            try
            {
                var result = false;

                if (umrahGuideComplete == null)
                    return result;
                var addUmrahGuideComplete = new UmrahGuideComplete();
                addUmrahGuideComplete.UmrahGuideCompleteId = umrahGuideComplete.UmrahGuideCompleteId;
                addUmrahGuideComplete.UmrahGuideId = umrahGuideComplete.UmrahGuideId;
                addUmrahGuideComplete.UmrahGuideCompleteByUserId = umrahGuideComplete.UmrahGuideCompleteByUserId;
                //Default Values
                addUmrahGuideComplete.IsActive = true;
                addUmrahGuideComplete.CreatedBy = userId;
                addUmrahGuideComplete.CreatedOn = DateTime.Now;
                addUmrahGuideComplete.UpdatedBy = userId;
                addUmrahGuideComplete.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahGuideCompleteRepository.AddUmrahGuideComplete(addUmrahGuideComplete);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<UmrahGuideCompleteDTO> GetAllUmrahGuideCompleteByUser(string umrahGuideCompleteUser)
        {
            try
            {
                List<UmrahGuideCompleteDTO> UmrahGuideCompleteResponse = new List<UmrahGuideCompleteDTO>();
                var allUmrahGuideCompletes = _unitOfWork.umrahGuideCompleteRepository.GetAllUmrahGuideCompleteByUser(umrahGuideCompleteUser);
                if (allUmrahGuideCompletes.Count() == 0)
                    return null;
                foreach (var UmrahGuideComplete in allUmrahGuideCompletes)
                {
                    UmrahGuideCompleteDTO umrahGuideComplete = new UmrahGuideCompleteDTO();
                    umrahGuideComplete.UmrahGuideCompleteId = UmrahGuideComplete.UmrahGuideCompleteId;
                    umrahGuideComplete.UmrahGuideId = UmrahGuideComplete.UmrahGuideId;
                    umrahGuideComplete.UmrahGuideCompleteByUserId = UmrahGuideComplete.UmrahGuideCompleteByUserId;
                    UmrahGuideCompleteResponse.Add(umrahGuideComplete);
                }
                return UmrahGuideCompleteResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}