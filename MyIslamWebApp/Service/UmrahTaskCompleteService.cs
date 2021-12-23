using MyIslamWebApp.DataTransferObjects.UmrahTaskComplete;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyIslamWebApp.Service
{
    public class UmrahTaskCompleteService : IUmrahTaskCompleteService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public UmrahTaskCompleteService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public UmrahTaskCompleteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region UmrahTaskComplete Methods
        public IEnumerable<UmrahTaskCompleteDTO> GetAllUmrahTaskComplete()
        {
            try
            {
                var response = new List<UmrahTaskCompleteDTO>();
                UmrahTaskCompleteDTO UmrahTaskCompleteResponse;
                var allUmrahTaskComplete = _unitOfWork.umrahTaskCompleteRepository.GetAllUmrahTaskComplete();
                if (allUmrahTaskComplete == null)
                    return response;
                foreach (var UmrahTaskComplete in allUmrahTaskComplete)
                {
                    if (UmrahTaskComplete.IsActive)
                    {
                        UmrahTaskCompleteResponse = new UmrahTaskCompleteDTO();
                        UmrahTaskCompleteResponse.UmrahTaskId = UmrahTaskComplete.UmrahTaskId;
                        UmrahTaskCompleteResponse.UmrahTaskCompleteId = UmrahTaskComplete.UmrahTaskCompleteId;
                        UmrahTaskCompleteResponse.UmrahTaskCompleteByUserId = UmrahTaskComplete.UmrahTaskCompleteByUserId;
                        response.Add(UmrahTaskCompleteResponse);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUmrahTaskComplete(int umrahTaskCompleteId)
        {
            try
            {
                var result = false;
                if (umrahTaskCompleteId == 0)
                    return result;

                result = _unitOfWork.umrahTaskCompleteRepository.DeleteUmrahTaskComplete(umrahTaskCompleteId);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddUmrahTaskComplete(UmrahTaskCompleteDTO umrahTaskComplete, string userId)
        {
            try
            {
                var result = false;

                if (umrahTaskComplete == null)
                    return result;
                var addUmrahTaskComplete = new UmrahTaskComplete();
                addUmrahTaskComplete.UmrahTaskCompleteId = umrahTaskComplete.UmrahTaskCompleteId;
                addUmrahTaskComplete.UmrahTaskId = umrahTaskComplete.UmrahTaskId;
                addUmrahTaskComplete.UmrahTaskCompleteByUserId = umrahTaskComplete.UmrahTaskCompleteByUserId;
                //Default Values
                addUmrahTaskComplete.IsActive = true;
                addUmrahTaskComplete.CreatedBy = userId;
                addUmrahTaskComplete.CreatedOn = DateTime.Now;
                addUmrahTaskComplete.UpdatedBy = userId;
                addUmrahTaskComplete.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahTaskCompleteRepository.AddUmrahTaskComplete(addUmrahTaskComplete);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<UmrahTaskCompleteDTO> GetAllUmrahTaskCompleteByUser(string umrahTaskCompleteUser)
        {
            try
            {
                List<UmrahTaskCompleteDTO> UmrahTaskCompleteResponse = new List<UmrahTaskCompleteDTO>();
                var allUmrahTaskCompletes = _unitOfWork.umrahTaskCompleteRepository.GetAllUmrahTaskCompleteByUser(umrahTaskCompleteUser);
                if (allUmrahTaskCompletes.Count() == 0)
                    return null;
                foreach (var UmrahTaskComplete in allUmrahTaskCompletes)
                {
                    UmrahTaskCompleteDTO umrahTaskComplete = new UmrahTaskCompleteDTO();
                    umrahTaskComplete.UmrahTaskCompleteId = UmrahTaskComplete.UmrahTaskCompleteId;
                    umrahTaskComplete.UmrahTaskId = UmrahTaskComplete.UmrahTaskId;
                    umrahTaskComplete.UmrahTaskCompleteByUserId = UmrahTaskComplete.UmrahTaskCompleteByUserId;
                    UmrahTaskCompleteResponse.Add(umrahTaskComplete);
                }
                return UmrahTaskCompleteResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}