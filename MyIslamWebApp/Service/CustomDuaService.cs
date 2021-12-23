using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.CustomDua;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class CustomDuaService : ICustomDuaService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public CustomDuaService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public CustomDuaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region CustomDua Methods
        public bool AddCustomDua(CustomDuaDTO customDua, string userId)
        {
            try
            {
                var result = false;

                if (customDua == null)
                    return result;

                var addCustomDua = new CustomDua();
                addCustomDua.CustomDuaId = customDua.CustomDuaId;              
                addCustomDua.CustomDuaName = customDua.CustomDuaName;
                addCustomDua.CustomDuaText = customDua.CustomDuaText;
              

                //Default Values
                addCustomDua.IsActive = true;
                addCustomDua.CreatedBy = userId;
                addCustomDua.CreatedOn = DateTime.Now;
                addCustomDua.UpdatedBy = userId;
                addCustomDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.customDuaRepository.AddCustomDua(addCustomDua);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateCustomDua(CustomDuaDTO customDua, string userId)
        {
            try
            {
                var result = false;
                if (customDua == null)
                    return result;

                var _customDua = _unitOfWork.customDuaRepository.GetCustomDuaById(customDua.CustomDuaId);

                if (_customDua == null)
                    return result;

                var updateCustomDua = new CustomDua();
                updateCustomDua.CustomDuaId = customDua.CustomDuaId;
                updateCustomDua.CustomDuaName = customDua.CustomDuaName;
                updateCustomDua.CustomDuaText = customDua.CustomDuaText;
             
                ////Default Values
                updateCustomDua.IsActive = _customDua.IsActive;
                updateCustomDua.CreatedBy = _customDua.CreatedBy;
                updateCustomDua.CreatedOn = _customDua.CreatedOn;
                updateCustomDua.UpdatedBy = userId;
                updateCustomDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.customDuaRepository.UpdateCustomDua(updateCustomDua);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCustomDua(CustomDuaDTO customDua, string userId)
        {
            try
            {
                var result = false;
                if (customDua == null)
                    return result;

                var deleteCustomDua = new CustomDua();
                deleteCustomDua.CustomDuaId = customDua.CustomDuaId;
                deleteCustomDua.CustomDuaName = customDua.CustomDuaName;
                deleteCustomDua.CustomDuaText = customDua.CustomDuaText;

                deleteCustomDua.IsActive = false;
                deleteCustomDua.UpdatedBy = userId;
                deleteCustomDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.customDuaRepository.DeleteCustomDua(deleteCustomDua);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCustomDuaById(int customDuaID, string userId)
        {
            try
            {
                var result = false;
                if (customDuaID == 0)
                    return result;

                var customDua = _unitOfWork.customDuaRepository.GetCustomDuaById(customDuaID);

                if (customDua == null)
                    return result;

                var deleteCustomDua = new CustomDua();
                deleteCustomDua = customDua;

                ////Default Values
                deleteCustomDua.IsActive = false;
                deleteCustomDua.UpdatedBy = userId;
                deleteCustomDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.customDuaRepository.DeleteCustomDua(deleteCustomDua);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CustomDuaDTO GetCustomDuaById(int customDuaID)
        {
            try
            {
                CustomDuaDTO customDuaResponse = new CustomDuaDTO();
                var customDua = _unitOfWork.customDuaRepository.GetCustomDuaById(customDuaID);
                if (customDua == null)
                    return null;

                if (customDua.IsActive)
                {
                    customDuaResponse.CustomDuaId = customDua.CustomDuaId;
                    customDuaResponse.CustomDuaName = customDua.CustomDuaName;
                    customDuaResponse.CustomDuaText = customDua.CustomDuaText;
                 
                }
                return customDuaResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<CustomDuaDTO> GetAllCustomDuas(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<CustomDuaDTO>();
                var customDuasList = new List<CustomDuaDTO>();
                CustomDuaDTO customDuaResponse;
                var allCustomDuas = _unitOfWork.customDuaRepository.GetAllCustomDuas(pageIndex, pageSize);

                if (allCustomDuas.TotalCount == 0)
                    return response;

                foreach (var customDua in allCustomDuas.Response)
                {
                    if (customDua.IsActive)
                    {
                        customDuaResponse = new CustomDuaDTO();
                        customDuaResponse.CustomDuaId = customDua.CustomDuaId;
                        customDuaResponse.CustomDuaName = customDua.CustomDuaName;
                        customDuaResponse.CustomDuaText = customDua.CustomDuaText;                       
                        customDuasList.Add(customDuaResponse);
                    }
                }
                response.Response = customDuasList;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CustomDuaDTO> GetAllCustomDuaByUserId(string userId)
        {
            try
            {
                var allCustomDuas = _unitOfWork.customDuaRepository.GetAllCustomDuaByUserId(userId);
                return allCustomDuas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}