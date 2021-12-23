using System;
using System.Collections.Generic;
using MyIslamWebApp.DataTransferObjects.DailyQuotes;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class DailyQuoteService : IDailyQuoteService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DailyQuoteService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public DailyQuoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region DailyQuote Methods
        public bool AddDailyQuote(DailyQuoteDTO dailyQuote, string userId)
        {
            try
            {
                var result = false;
                if (dailyQuote == null)
                    return result;

                var addDailyQuote = new DailyQuote();
                addDailyQuote.DailyQuoteId = dailyQuote.DailyQuoteId;
                addDailyQuote.DailyQuoteText = dailyQuote.DailyQuoteText;
                addDailyQuote.DailyQuoteLanguage = dailyQuote.DailyQuoteLanguage;
                addDailyQuote.DailyQuoteValidOn = dailyQuote.DailyQuoteValidOn;

                //Default Values
                addDailyQuote.IsActive = true;
                addDailyQuote.CreatedBy = userId;
                addDailyQuote.CreatedOn = DateTime.Now;
                addDailyQuote.UpdatedBy = userId;
                addDailyQuote.UpdatedOn = DateTime.Now;
                result = _unitOfWork.dailyQuoteRepository.AddDailyQuote(addDailyQuote);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateDailyQuote(DailyQuoteDTO dailyQuote, string userId)
        {
            try
            {
                var result = false;
                if (dailyQuote == null)
                    return result;

                var _dailyQuotes = _unitOfWork.dailyQuoteRepository.GetDailyQuoteById(dailyQuote.DailyQuoteId);

                if (_dailyQuotes == null)
                    return result;

                var updateDailyQuote = new DailyQuote();
                updateDailyQuote.DailyQuoteId = dailyQuote.DailyQuoteId;
                updateDailyQuote.DailyQuoteText = dailyQuote.DailyQuoteText;
                updateDailyQuote.DailyQuoteLanguage = dailyQuote.DailyQuoteLanguage;
                updateDailyQuote.DailyQuoteValidOn = dailyQuote.DailyQuoteValidOn;
                result = _unitOfWork.dailyQuoteRepository.UpdateDailyQuote(updateDailyQuote);

                ////Default Values
                updateDailyQuote.IsActive = _dailyQuotes.IsActive;
                updateDailyQuote.CreatedBy = _dailyQuotes.CreatedBy;
                updateDailyQuote.CreatedOn = _dailyQuotes.CreatedOn;
                updateDailyQuote.UpdatedBy = userId;
                updateDailyQuote.UpdatedOn = DateTime.Now;

                result = _unitOfWork.dailyQuoteRepository.UpdateDailyQuote(updateDailyQuote);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDailyQuote(DailyQuoteDTO dailyQuote, string userId)
        {
            try
            {
                var result = false;
                if (dailyQuote == null)
                    return result;

                var deleteDailyQuote = new DailyQuote();
                deleteDailyQuote.DailyQuoteId = dailyQuote.DailyQuoteId;
                deleteDailyQuote.DailyQuoteLanguage = dailyQuote.DailyQuoteLanguage;
                deleteDailyQuote.DailyQuoteText = dailyQuote.DailyQuoteText;
                deleteDailyQuote.DailyQuoteValidOn = dailyQuote.DailyQuoteValidOn;

                ////Default Values
                deleteDailyQuote.IsActive = false;
                deleteDailyQuote.UpdatedBy = userId;
                deleteDailyQuote.UpdatedOn = DateTime.Now;

                result = _unitOfWork.dailyQuoteRepository.DeleteDailyQuote(deleteDailyQuote);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDailyQuoteById(int dailyQuoteID, string userId)
        {
            try
            {
                var result = false;
                if (dailyQuoteID == 0)
                    return result;

                var dailyQuote = _unitOfWork.dailyQuoteRepository.GetDailyQuoteById(dailyQuoteID);

                if (dailyQuote == null)
                    return result;

                var deletedailyQuote = new DailyQuote();
                deletedailyQuote = dailyQuote;
                ////Default Values
                deletedailyQuote.IsActive = false;
                deletedailyQuote.UpdatedBy = userId;
                deletedailyQuote.UpdatedOn = DateTime.Now;

                result = _unitOfWork.dailyQuoteRepository.DeleteDailyQuote(deletedailyQuote);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DailyQuoteDTO GetDailyQuoteById(int dailyQuoteID)
        {
            try
            {
                DailyQuoteDTO dailyQuoteResponse = new DailyQuoteDTO();
                var dailyQuote = _unitOfWork.dailyQuoteRepository.GetDailyQuoteById(dailyQuoteID);
                if (dailyQuote == null)
                    return null;

                if (dailyQuote.IsActive)
                {
                    dailyQuoteResponse.DailyQuoteId = dailyQuote.DailyQuoteId;
                    dailyQuoteResponse.DailyQuoteText = dailyQuote.DailyQuoteText;
                    dailyQuoteResponse.DailyQuoteLanguage = dailyQuote.DailyQuoteLanguage;
                    dailyQuoteResponse.DailyQuoteValidOn = dailyQuote.DailyQuoteValidOn;
                }
                return dailyQuoteResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<DailyQuoteDTO> GetAllDailyQuotes(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<DailyQuoteDTO>();
                var dailyQuotesList = new List<DailyQuoteDTO>();
                DailyQuoteDTO dailyQuoteResponse;
                var allDailyQuotes = _unitOfWork.dailyQuoteRepository.GetAllDailyQuotes(pageIndex, pageSize);

                if (allDailyQuotes.TotalCount == 0)
                    return response;

                foreach (var dailyQuote in allDailyQuotes.Response)
                {
                    if (dailyQuote.IsActive)
                    {
                        dailyQuoteResponse = new DailyQuoteDTO();
                        dailyQuoteResponse.DailyQuoteId = dailyQuote.DailyQuoteId;
                        dailyQuoteResponse.DailyQuoteText = dailyQuote.DailyQuoteText;
                        dailyQuoteResponse.DailyQuoteLanguage = dailyQuote.DailyQuoteLanguage;
                        dailyQuoteResponse.DailyQuoteValidOn = dailyQuote.DailyQuoteValidOn; ;
                        dailyQuotesList.Add(dailyQuoteResponse);
                    }
                }
                response.Response = dailyQuotesList;
                response.TotalCount = allDailyQuotes.TotalCount;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DailyQuoteDTO GetDailyQuoteByLang_Date(int languageCode, DateTime utcDateTime)
        {
            try
            {
                DailyQuoteDTO dailyQuoteResponse = new DailyQuoteDTO();
                var dailyQuote = _unitOfWork.dailyQuoteRepository.GetDailyQuoteByLang_Date(languageCode, utcDateTime);
                if (dailyQuote == null)
                    return null;

                if (dailyQuote.IsActive)
                {
                    dailyQuoteResponse.DailyQuoteId = dailyQuote.DailyQuoteId;
                    dailyQuoteResponse.DailyQuoteLanguage = dailyQuote.DailyQuoteLanguage;
                    dailyQuoteResponse.DailyQuoteText = dailyQuote.DailyQuoteText;
                    dailyQuoteResponse.DailyQuoteValidOn = dailyQuote.DailyQuoteValidOn;
                }
                return dailyQuoteResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        #endregion

    }
}