using MyIslamWebApp.DataTransferObjects.HajjTaskComplete;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyIslamWebApp.Service
{
    public class HajjTaskCompleteService : IHajjTaskCompleteService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public HajjTaskCompleteService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public HajjTaskCompleteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region HajjTaskComplete Methods       

        public bool AddHajjTaskComplete(HajjTaskCompleteDTO hajjTaskComplete, string userId)
        {
            try
            {
                var result = false;

                if (hajjTaskComplete == null)
                    return result;
                var addHajjTaskComplete = new HajjTaskComplete();
                addHajjTaskComplete.HajjTaskCompleteId = hajjTaskComplete.HajjTaskCompleteId;
                addHajjTaskComplete.HajjTaskId = hajjTaskComplete.HajjTaskId;                
                addHajjTaskComplete.HajjTaskCompleteByUserId = hajjTaskComplete.HajjTaskCompleteByUserId;
                //Default Values
                addHajjTaskComplete.IsActive = true;
                addHajjTaskComplete.CreatedBy = userId;
                addHajjTaskComplete.CreatedOn = DateTime.Now;
                addHajjTaskComplete.UpdatedBy = userId;
                addHajjTaskComplete.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjTaskCompleteRepository.AddHajjTaskComplete(addHajjTaskComplete);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteHajjTaskComplete(int hajjTaskCompleteId)
        {
            try
            {
                var result = false;
                if (hajjTaskCompleteId == 0)
                    return result;

                result = _unitOfWork.hajjTaskCompleteRepository.DeleteHajjTaskComplete(hajjTaskCompleteId);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public IEnumerable<HajjTaskCompleteDTO> GetAllHajjTaskComplete()
        {
            try
            {
                var response = new List<HajjTaskCompleteDTO>();
                HajjTaskCompleteDTO HajjTaskCompleteResponse;
                var allHajjTaskComplete = _unitOfWork.hajjTaskCompleteRepository.GetAllHajjTaskComplete();
                if (allHajjTaskComplete == null)
                    return response;
                foreach (var HajjTaskComplete in allHajjTaskComplete)
                {
                    if (HajjTaskComplete.IsActive)
                    {
                        HajjTaskCompleteResponse = new HajjTaskCompleteDTO();
                        HajjTaskCompleteResponse.HajjTaskId = HajjTaskComplete.HajjTaskId;
                        HajjTaskCompleteResponse.HajjTaskCompleteId = HajjTaskComplete.HajjTaskCompleteId;
                        HajjTaskCompleteResponse.HajjTaskCompleteByUserId = HajjTaskComplete.HajjTaskCompleteByUserId;
                        response.Add(HajjTaskCompleteResponse);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<HajjTaskCompleteDTO> GetAllHajjTaskCompleteByUser(string hajjTaskCompleteUser)
        {
            try
            {
                List<HajjTaskCompleteDTO> HajjTaskCompleteResponse = new List<HajjTaskCompleteDTO>();
                var allHajjTaskCompletes = _unitOfWork.hajjTaskCompleteRepository.GetAllHajjTaskCompleteByUser(hajjTaskCompleteUser);
                if (allHajjTaskCompletes.Count() == 0)
                    return null;
                foreach (var HajjTaskComplete in allHajjTaskCompletes)
                {
                    HajjTaskCompleteDTO hajjTaskComplete = new HajjTaskCompleteDTO();
                    hajjTaskComplete.HajjTaskCompleteId = HajjTaskComplete.HajjTaskCompleteId;
                    hajjTaskComplete.HajjTaskId = HajjTaskComplete.HajjTaskId;
                    hajjTaskComplete.HajjTaskCompleteByUserId = HajjTaskComplete.HajjTaskCompleteByUserId;
                    HajjTaskCompleteResponse.Add(hajjTaskComplete);
                }
                return HajjTaskCompleteResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}