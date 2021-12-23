using MyIslamWebApp.DataTransferObjects.HajjGuideComplete;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyIslamWebApp.Service
{
    public class HajjGuideCompleteService : IHajjGuideCompleteService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public HajjGuideCompleteService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public HajjGuideCompleteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region HajjGuideComplete Methods
        public IEnumerable<HajjGuideCompleteDTO> GetAllHajjGuideComplete()
        {
            try
            {
                var response = new List<HajjGuideCompleteDTO>();
                HajjGuideCompleteDTO HajjGuideCompleteResponse;
                var allHajjGuideComplete = _unitOfWork.hajjGuideCompleteRepository.GetAllHajjGuideComplete();
                if (allHajjGuideComplete == null)
                    return response;
                foreach (var HajjGuideComplete in allHajjGuideComplete)
                {
                    if (HajjGuideComplete.IsActive)
                    {
                        HajjGuideCompleteResponse = new HajjGuideCompleteDTO();
                        HajjGuideCompleteResponse.HajjGuideId = HajjGuideComplete.HajjGuideId;
                        HajjGuideCompleteResponse.HajjGuideCompleteId = HajjGuideComplete.HajjGuideCompleteId;
                        HajjGuideCompleteResponse.HajjGuideCompleteByUserId = HajjGuideComplete.HajjGuideCompleteByUserId;
                        response.Add(HajjGuideCompleteResponse);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteHajjGuideComplete(int hajjGuideCompleteId)
        {
            try
            {
                var result = false;
                if (hajjGuideCompleteId == 0)
                    return result;

                result = _unitOfWork.hajjGuideCompleteRepository.DeleteHajjGuideComplete(hajjGuideCompleteId);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddHajjGuideComplete(HajjGuideCompleteDTO hajjGuideComplete, string userId)
        {
            try
            {
                var result = false;

                if (hajjGuideComplete == null)
                    return result;
                var addHajjGuideComplete = new HajjGuideComplete();
                addHajjGuideComplete.HajjGuideCompleteId = hajjGuideComplete.HajjGuideCompleteId;
                addHajjGuideComplete.HajjGuideId = hajjGuideComplete.HajjGuideId;
                addHajjGuideComplete.HajjGuideCompleteByUserId = hajjGuideComplete.HajjGuideCompleteByUserId;
                //Default Values
                addHajjGuideComplete.IsActive = true;
                addHajjGuideComplete.CreatedBy = userId;
                addHajjGuideComplete.CreatedOn = DateTime.Now;
                addHajjGuideComplete.UpdatedBy = userId;
                addHajjGuideComplete.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjGuideCompleteRepository.AddHajjGuideComplete(addHajjGuideComplete);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<HajjGuideCompleteDTO> GetAllHajjGuideCompleteByUser(string hajjGuideCompleteUser)
        {
            try
            {
                List<HajjGuideCompleteDTO> HajjGuideCompleteResponse = new List<HajjGuideCompleteDTO>();
                var allHajjGuideCompletes = _unitOfWork.hajjGuideCompleteRepository.GetAllHajjGuideCompleteByUser(hajjGuideCompleteUser);
                if (allHajjGuideCompletes.Count() == 0)
                    return null;
                foreach (var HajjGuideComplete in allHajjGuideCompletes)
                {
                    HajjGuideCompleteDTO hajjGuideComplete = new HajjGuideCompleteDTO();
                    hajjGuideComplete.HajjGuideCompleteId = HajjGuideComplete.HajjGuideCompleteId;
                    hajjGuideComplete.HajjGuideId = HajjGuideComplete.HajjGuideId;
                    hajjGuideComplete.HajjGuideCompleteByUserId = HajjGuideComplete.HajjGuideCompleteByUserId;
                    HajjGuideCompleteResponse.Add(hajjGuideComplete);
                }
                return HajjGuideCompleteResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}