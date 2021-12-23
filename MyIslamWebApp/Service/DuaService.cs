using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.Dua;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class DuaService : IDuaService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DuaService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public DuaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Dua Methods
        public bool AddDua(DuaDTO dua, string userId) 
        {
            try
            {
                var result = false;

                if (dua == null)
                    return result;

                var addDua = new Dua();
                addDua.DuaId = dua.DuaId;
                addDua.DuaCategoryId = dua.DuaCategoryId;
                addDua.DuaName = dua.DuaName;
                addDua.DuaArabicText = dua.DuaArabicText;
                addDua.DuaEnglishText = dua.DuaEnglishText;
                addDua.DuaTurkeyText = dua.DuaTurkeyText;
                addDua.DuaMalayText = dua.DuaMalayText;
                addDua.DuaPronunciationText = dua.DuaPronunciationText;
                //Default Values
                addDua.IsActive = true;
                addDua.CreatedBy = userId;
                addDua.CreatedOn = DateTime.Now;
                addDua.UpdatedBy = userId;
                addDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.duaRepository.AddDua(addDua);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateDua(DuaDTO dua, string userId)
        {
            try
            {
                var result = false;
                if (dua == null)
                    return result;

                var _dua = _unitOfWork.duaRepository.GetDuaById(dua.DuaId);

                if (_dua == null)
                    return result;

                var updateDua = new Dua();
                updateDua.DuaId = dua.DuaId;
                updateDua.DuaCategoryId = dua.DuaCategoryId;
                updateDua.DuaName = dua.DuaName;
                updateDua.DuaArabicText = dua.DuaArabicText;
                updateDua.DuaEnglishText = dua.DuaEnglishText;
                updateDua.DuaTurkeyText = dua.DuaTurkeyText;
                updateDua.DuaMalayText = dua.DuaMalayText;

                ////Default Values
                updateDua.IsActive = _dua.IsActive;
                updateDua.CreatedBy = _dua.CreatedBy;
                updateDua.CreatedOn = _dua.CreatedOn;
                updateDua.UpdatedBy = userId;
                updateDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.duaRepository.UpdateDua(updateDua);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDua(DuaDTO dua, string userId)
        {
            try
            {
                var result = false;
                if (dua == null)
                    return result;

                var deleteDua = new Dua();
                deleteDua.DuaId = dua.DuaId;
                deleteDua.DuaCategoryId = dua.DuaCategoryId;
                deleteDua.DuaName = dua.DuaName;
                deleteDua.DuaArabicText = dua.DuaArabicText;
                deleteDua.DuaEnglishText = dua.DuaEnglishText;
                deleteDua.DuaTurkeyText = dua.DuaTurkeyText;
                deleteDua.DuaMalayText = dua.DuaMalayText;

                deleteDua.IsActive = false;
                deleteDua.UpdatedBy = userId;
                deleteDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.duaRepository.DeleteDua(deleteDua);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDuaById(int duaID, string userId)
        {
            try
            {
                var result = false;
                if (duaID == 0)
                    return result;

                var dua = _unitOfWork.duaRepository.GetDuaById(duaID);

                if (dua == null)
                    return result;

                var deleteDua = new Dua();
                deleteDua = dua;

                ////Default Values
                deleteDua.IsActive = false;
                deleteDua.UpdatedBy = userId;
                deleteDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.duaRepository.DeleteDua(deleteDua);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DuaDTO GetDuaById(int duaID)
        {
            try
            {
                DuaDTO duaResponse = new DuaDTO();
                var dua = _unitOfWork.duaRepository.GetDuaById(duaID);
                if (dua == null)
                    return null;

                if (dua.IsActive)
                {
                    duaResponse.DuaId = dua.DuaId;
                    duaResponse.DuaCategoryId = dua.DuaCategoryId;
                    duaResponse.DuaName = dua.DuaName;
                    duaResponse.DuaArabicText = dua.DuaArabicText;
                    duaResponse.DuaEnglishText = dua.DuaEnglishText;
                    duaResponse.DuaTurkeyText = dua.DuaTurkeyText;
                    duaResponse.DuaMalayText = dua.DuaMalayText;
                }
                return duaResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<DuaDTO> GetAllDuas(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<DuaDTO>();
                var duasList = new List<DuaDTO>();
                DuaDTO duaResponse;                
                var allDuas = _unitOfWork.duaRepository.GetAllDuas(pageIndex, pageSize);

                if (allDuas.TotalCount == 0)
                    return response;

                foreach (var dua in allDuas.Response)
                {
                    if (dua.IsActive)
                    {
                        duaResponse = new DuaDTO();
                        duaResponse.DuaId = dua.DuaId;
                        duaResponse.DuaCategoryId = dua.DuaCategoryId;
                        duaResponse.DuaName = dua.DuaName;
                        duaResponse.DuaArabicText = dua.DuaArabicText;
                        duaResponse.DuaEnglishText = dua.DuaEnglishText;
                        duaResponse.DuaMalayText = dua.DuaMalayText;
                        duaResponse.DuaTurkeyText = dua.DuaTurkeyText;
                        duaResponse.DuaPronunciationText = dua.DuaPronunciationText;
                        duasList.Add(duaResponse);
                    }
                }
                response.Response = duasList;
                response.TotalCount = allDuas.TotalCount;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DuaDTO> GetAllDuaByCategoryId(int duaCategoryId)
        {
            try
            {
                var allDuaByCategoryId = _unitOfWork.duaRepository.GetAllDuaByCategoryId(duaCategoryId);
                return allDuaByCategoryId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}