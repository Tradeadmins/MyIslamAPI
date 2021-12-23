using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.UmrahGuide;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class UmrahGuideService : IUmrahGuideService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public UmrahGuideService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public UmrahGuideService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region UmrahGuide Methods
        public bool AddUmrahGuide(UmrahGuideDTO umrahGuide, string userId)
        {
            try
            {
                var result = false;

                if (umrahGuide == null)
                    return result;

                var addUmrahGuide = new UmrahGuide();
                addUmrahGuide.UmrahGuideId = umrahGuide.UmrahGuideId;
                addUmrahGuide.UmrahGuideName = umrahGuide.UmrahGuideName;
                //Default Values
                addUmrahGuide.IsActive = true;
                addUmrahGuide.CreatedBy = userId;
                addUmrahGuide.CreatedOn = DateTime.Now;
                addUmrahGuide.UpdatedBy = userId;
                addUmrahGuide.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahGuideRepository.AddUmrahGuide(addUmrahGuide);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateUmrahGuide(UmrahGuideDTO umrahGuide, string userId)
        {
            try
            {
                var result = false;
                if (umrahGuide == null)
                    return result;

                var _umrahGuide = _unitOfWork.umrahGuideRepository.GetUmrahGuideById(umrahGuide.UmrahGuideId);

                if (_umrahGuide == null)
                    return result;

                var updateUmrahGuide = new UmrahGuide();
                updateUmrahGuide.UmrahGuideId = umrahGuide.UmrahGuideId;
                updateUmrahGuide.UmrahGuideName = umrahGuide.UmrahGuideName;

                ////Default Values
                updateUmrahGuide.IsActive = _umrahGuide.IsActive;
                updateUmrahGuide.CreatedBy = _umrahGuide.CreatedBy;
                updateUmrahGuide.CreatedOn = _umrahGuide.CreatedOn;
                updateUmrahGuide.UpdatedBy = userId;
                updateUmrahGuide.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahGuideRepository.UpdateUmrahGuide(updateUmrahGuide);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUmrahGuide(UmrahGuideDTO umrahGuide, string userId)
        {
            try
            {
                var result = false;
                if (umrahGuide == null)
                    return result;

                var deleteUmrahGuide = new UmrahGuide();
                deleteUmrahGuide.UmrahGuideId = umrahGuide.UmrahGuideId;
                deleteUmrahGuide.UmrahGuideName = umrahGuide.UmrahGuideName;

                deleteUmrahGuide.IsActive = false;
                deleteUmrahGuide.UpdatedBy = userId;
                deleteUmrahGuide.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahGuideRepository.DeleteUmrahGuide(deleteUmrahGuide);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUmrahGuideById(int umrahGuideID, string userId)
        {
            try
            {
                var result = false;
                if (umrahGuideID == 0)
                    return result;

                var umrahGuide = _unitOfWork.umrahGuideRepository.GetUmrahGuideById(umrahGuideID);

                if (umrahGuide == null)
                    return result;

                var deleteUmrahGuide = new UmrahGuide();
                deleteUmrahGuide = umrahGuide;

                ////Default Values
                deleteUmrahGuide.IsActive = false;
                deleteUmrahGuide.UpdatedBy = userId;
                deleteUmrahGuide.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahGuideRepository.DeleteUmrahGuide(deleteUmrahGuide);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UmrahGuideDTO GetUmrahGuideById(int umrahGuideID)
        {
            try
            {
                UmrahGuideDTO umrahGuideResponse = new UmrahGuideDTO();
                var umrahGuide = _unitOfWork.umrahGuideRepository.GetUmrahGuideById(umrahGuideID);
                if (umrahGuide == null)
                    return null;

                if (umrahGuide.IsActive)
                {
                    umrahGuideResponse.UmrahGuideId = umrahGuide.UmrahGuideId;
                    umrahGuideResponse.UmrahGuideName = umrahGuide.UmrahGuideName;
                }
                return umrahGuideResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<UmrahGuideListDTO> GetAllUmrahGuides(int pageIndex, int pageSize, string userId)
        {
            try
            {
                var response = new ResultDTO<UmrahGuideListDTO>();
                List<UmrahGuideListDTO> umrahGuideDTOList = new List<UmrahGuideListDTO>();
                UmrahGuideListDTO umrahGuideResponse;

                var allUmrahGuides = _unitOfWork.umrahGuideRepository.GetAllUmrahGuides(pageIndex, pageSize, userId);

                if (allUmrahGuides == null)
                    return response;

                foreach (var umrahGuide in allUmrahGuides.Response)
                {
                    umrahGuideResponse = new UmrahGuideListDTO();
                    umrahGuideResponse.UmrahGuideId = umrahGuide.UmrahGuideId;
                    umrahGuideResponse.UmrahGuideName = umrahGuide.UmrahGuideName;
                    umrahGuideResponse.UmrahGuideIsCompleted = umrahGuide.UmrahGuideIsCompleted;
                    umrahGuideDTOList.Add(umrahGuideResponse);
                }
                response.Response = umrahGuideDTOList;
                response.TotalCount = allUmrahGuides.TotalCount;
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