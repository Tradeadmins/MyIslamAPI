using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.HajjGuide;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class HajjGuideService : IHajjGuideService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public HajjGuideService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public HajjGuideService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region HajjGuide Methods
        public bool AddHajjGuide(HajjGuideDTO hajjGuide, string userId)
        {
            try
            {
                var result = false;

                if (hajjGuide == null)
                    return result;

                var addHajjGuide = new HajjGuide();
                addHajjGuide.HajjGuideId = hajjGuide.HajjGuideId;
                addHajjGuide.HajjGuideName = hajjGuide.HajjGuideName;
                //Default Values
                addHajjGuide.IsActive = true;
                addHajjGuide.CreatedBy = userId;
                addHajjGuide.CreatedOn = DateTime.Now;
                addHajjGuide.UpdatedBy = userId;
                addHajjGuide.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjGuideRepository.AddHajjGuide(addHajjGuide);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateHajjGuide(HajjGuideDTO hajjGuide, string userId)
        {
            try
            {
                var result = false;
                if (hajjGuide == null)
                    return result;

                var _hajjGuide = _unitOfWork.hajjGuideRepository.GetHajjGuideById(hajjGuide.HajjGuideId);

                if (_hajjGuide == null)
                    return result;

                var updateHajjGuide = new HajjGuide();
                updateHajjGuide.HajjGuideId = hajjGuide.HajjGuideId;
                updateHajjGuide.HajjGuideName = hajjGuide.HajjGuideName;

                ////Default Values
                updateHajjGuide.IsActive = _hajjGuide.IsActive;
                updateHajjGuide.CreatedBy = _hajjGuide.CreatedBy;
                updateHajjGuide.CreatedOn = _hajjGuide.CreatedOn;
                updateHajjGuide.UpdatedBy = userId;
                updateHajjGuide.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjGuideRepository.UpdateHajjGuide(updateHajjGuide);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteHajjGuide(HajjGuideDTO hajjGuide, string userId)
        {
            try
            {
                var result = false;
                if (hajjGuide == null)
                    return result;

                var deleteHajjGuide = new HajjGuide();
                deleteHajjGuide.HajjGuideId = hajjGuide.HajjGuideId;
                deleteHajjGuide.HajjGuideName = hajjGuide.HajjGuideName;

                deleteHajjGuide.IsActive = false;
                deleteHajjGuide.UpdatedBy = userId;
                deleteHajjGuide.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjGuideRepository.DeleteHajjGuide(deleteHajjGuide);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteHajjGuideById(int hajjGuideID, string userId)
        {
            try
            {
                var result = false;
                if (hajjGuideID == 0)
                    return result;

                var hajjGuide = _unitOfWork.hajjGuideRepository.GetHajjGuideById(hajjGuideID);

                if (hajjGuide == null)
                    return result;

                var deleteHajjGuide = new HajjGuide();
                deleteHajjGuide = hajjGuide;

                ////Default Values
                deleteHajjGuide.IsActive = false;
                deleteHajjGuide.UpdatedBy = userId;
                deleteHajjGuide.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjGuideRepository.DeleteHajjGuide(deleteHajjGuide);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HajjGuideDTO GetHajjGuideById(int hajjGuideID)
        {
            try
            {
                HajjGuideDTO hajjGuideResponse = new HajjGuideDTO();
                var hajjGuide = _unitOfWork.hajjGuideRepository.GetHajjGuideById(hajjGuideID);
                if (hajjGuide == null)
                    return null;

                if (hajjGuide.IsActive)
                {
                    hajjGuideResponse.HajjGuideId = hajjGuide.HajjGuideId;
                    hajjGuideResponse.HajjGuideName = hajjGuide.HajjGuideName;
                }
                return hajjGuideResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<HajjGuideListDTO> GetAllHajjGuides(int pageIndex, int pageSize, string userId)
        {
            try
            {
                var response = new ResultDTO<HajjGuideListDTO>();
                List<HajjGuideListDTO> hajjGuideDTOList = new List<HajjGuideListDTO>();
                HajjGuideListDTO hajjGuideResponse;

                var allHajjGuides = _unitOfWork.hajjGuideRepository.GetAllHajjGuides(pageIndex, pageSize, userId);

                if (allHajjGuides == null)
                    return response;

                foreach (var hajjGuide in allHajjGuides.Response)
                {
                    hajjGuideResponse = new HajjGuideListDTO();
                    hajjGuideResponse.HajjGuideId = hajjGuide.HajjGuideId;
                    hajjGuideResponse.HajjGuideName = hajjGuide.HajjGuideName;
                    hajjGuideResponse.HajjGuideCompleteId = hajjGuide.HajjGuideCompleteId;
                    hajjGuideResponse.HajjGuideIsCompleted = hajjGuide.HajjGuideIsCompleted;
                    hajjGuideDTOList.Add(hajjGuideResponse);
                }
                response.Response = hajjGuideDTOList;
                response.TotalCount = allHajjGuides.TotalCount;
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