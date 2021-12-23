using MyIslamWebApp.DataTransferObjects.MakeDua;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyIslamWebApp.Service
{
    public class MakeDuaService : IMakeDuaService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public MakeDuaService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public MakeDuaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region MakeDua Methods
        public IEnumerable<MakeDuaDTO> GetAllMakeDua()
        {
            try
            {
                var response = new List<MakeDuaDTO>();
                MakeDuaDTO MakeDuaResponse;
                var allMakeDua = _unitOfWork.MakeDuaRepository.GetAllMakeDua();
                if (allMakeDua == null)
                    return response;
                foreach (var MakeDua in allMakeDua)
                {
                    if (MakeDua.IsActive)
                    {
                        MakeDuaResponse = new MakeDuaDTO();
                        MakeDuaResponse.MakeDuaId = MakeDua.MakeDuaId;
                        MakeDuaResponse.MakeDuaPrayerRequestId = MakeDua.MakeDuaPrayerRequestId;
                        MakeDuaResponse.MakeDuaByUserId = MakeDua.MakeDuaByUserId;
                        response.Add(MakeDuaResponse);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddMakeDua(MakeDuaDTO makeDua, string userId)
        {
            try
            {
                var result = false;

                if (makeDua == null)
                    return result;
                var addMakeDua = new MakeDua();
                addMakeDua.MakeDuaPrayerRequestId = makeDua.MakeDuaPrayerRequestId;
                addMakeDua.MakeDuaByUserId = makeDua.MakeDuaByUserId;
                //Default Values
                addMakeDua.IsActive = true;
                addMakeDua.CreatedBy = userId;
                addMakeDua.CreatedOn = DateTime.Now;
                addMakeDua.UpdatedBy = userId;
                addMakeDua.UpdatedOn = DateTime.Now;

                result = _unitOfWork.MakeDuaRepository.AddMakeDua(addMakeDua);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<MakeDuaDTO> GetAllMakeDuaByUser(string MakeDuaUser)
        {
            try
            {
                List<MakeDuaDTO> MakeDuaResponse = new List<MakeDuaDTO>();
                var allMakeDuas = _unitOfWork.MakeDuaRepository.GetAllMakeDuaByUser(MakeDuaUser);
                if (allMakeDuas.Count() == 0)
                    return null;
                foreach (var MakeDua in allMakeDuas)
                {
                    MakeDuaDTO makeDua = new MakeDuaDTO();
                    makeDua.MakeDuaId = MakeDua.MakeDuaId;
                    makeDua.MakeDuaPrayerRequestId = MakeDua.MakeDuaPrayerRequestId;
                    MakeDuaResponse.Add(makeDua);
                }
                return MakeDuaResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}