using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.DuaCategory;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class DuaCategoryService : IDuaCategoryService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DuaCategoryService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public DuaCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region DuaCategory Methods
        public bool AddDuaCategory(DuaCategoryDTO duaCategory, string userId)
        {
            try
            {
                var result = false;

                if (duaCategory == null)
                    return result;

                var addDuaCategory = new DuaCategory();
                addDuaCategory.DuaCategoryId = duaCategory.DuaCategoryId;
                addDuaCategory.DuaCategoryName = duaCategory.DuaCategoryName;
                
                //Default Values
                addDuaCategory.IsActive = true;
                addDuaCategory.CreatedBy = userId;
                addDuaCategory.CreatedOn = DateTime.Now;
                addDuaCategory.UpdatedBy = userId;
                addDuaCategory.UpdatedOn = DateTime.Now;

                result = _unitOfWork.duaCategoryRepository.AddDuaCategory(addDuaCategory);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateDuaCategory(DuaCategoryDTO duaCategory, string userId)
        {
            try
            {
                var result = false;
                if (duaCategory == null)
                    return result;

                var _duaCategory = _unitOfWork.duaCategoryRepository.GetDuaCategoryById(duaCategory.DuaCategoryId);

                if (_duaCategory == null)
                    return result;

                var updateDuaCategory = new DuaCategory();
                updateDuaCategory.DuaCategoryId = duaCategory.DuaCategoryId;
                updateDuaCategory.DuaCategoryName = duaCategory.DuaCategoryName;
                
                ////Default Values
                updateDuaCategory.IsActive = _duaCategory.IsActive;
                updateDuaCategory.CreatedBy = _duaCategory.CreatedBy;
                updateDuaCategory.CreatedOn = _duaCategory.CreatedOn;
                updateDuaCategory.UpdatedBy = userId;
                updateDuaCategory.UpdatedOn = DateTime.Now;

                result = _unitOfWork.duaCategoryRepository.UpdateDuaCategory(updateDuaCategory);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDuaCategory(DuaCategoryDTO duaCategory, string userId)
        {
            try
            {
                var result = false;
                if (duaCategory == null)
                    return result;

                var deleteDuaCategory = new DuaCategory();
                deleteDuaCategory.DuaCategoryId = duaCategory.DuaCategoryId;
                deleteDuaCategory.DuaCategoryName = duaCategory.DuaCategoryName;
               
                deleteDuaCategory.IsActive = false;
                deleteDuaCategory.UpdatedBy = userId;
                deleteDuaCategory.UpdatedOn = DateTime.Now;

                result = _unitOfWork.duaCategoryRepository.DeleteDuaCategory(deleteDuaCategory);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDuaCategoryById(int duaCategoryID, string userId)
        {
            try
            {
                var result = false;
                if (duaCategoryID == 0)
                    return result;

                var duaCategory = _unitOfWork.duaCategoryRepository.GetDuaCategoryById(duaCategoryID);

                if (duaCategory == null)
                    return result;

                var deleteDuaCategory = new DuaCategory();
                deleteDuaCategory = duaCategory;

                ////Default Values
                deleteDuaCategory.IsActive = false;
                deleteDuaCategory.UpdatedBy = userId;
                deleteDuaCategory.UpdatedOn = DateTime.Now;

                result = _unitOfWork.duaCategoryRepository.DeleteDuaCategory(deleteDuaCategory);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DuaCategoryDTO GetDuaCategoryById(int duaCategoryID)
        {
            try
            {
                DuaCategoryDTO duaCategoryResponse = new DuaCategoryDTO();
                var duaCategory = _unitOfWork.duaCategoryRepository.GetDuaCategoryById(duaCategoryID);
                if (duaCategory == null)
                    return null;

                if (duaCategory.IsActive)
                {
                    duaCategoryResponse.DuaCategoryId = duaCategory.DuaCategoryId;
                    duaCategoryResponse.DuaCategoryName = duaCategory.DuaCategoryName;
                }
                return duaCategoryResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DuaCategoryDTO> GetAllDuaCategory()
        {
            try
            {
                var response = new List<DuaCategoryDTO>();
              
                DuaCategoryDTO duaCategoryResponse;
                var allDuaCategorys = _unitOfWork.duaCategoryRepository.GetAllDuaCategorys();

                if (allDuaCategorys == null)
                    return response;

                foreach (var duaCategory in allDuaCategorys)
                {
                    if (duaCategory.IsActive)
                    {
                        duaCategoryResponse = new DuaCategoryDTO();
                        duaCategoryResponse.DuaCategoryId = duaCategory.DuaCategoryId;
                        duaCategoryResponse.DuaCategoryName = duaCategory.DuaCategoryName;
                        response.Add(duaCategoryResponse);
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