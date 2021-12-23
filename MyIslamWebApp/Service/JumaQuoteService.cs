using System;
using System.Collections.Generic;
using MyIslamWebApp.DataTransferObjects.JumaQuote;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class JumaQuoteService : IJumaQuoteService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public JumaQuoteService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public JumaQuoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region JumaQuote Methods
        public bool AddJumaQuote(JumaQuoteDTO jumaQuote, string userId)
        {
            try
            {
                var result = false;

                if (jumaQuote == null)
                    return result;

                var addJumaQuote = new JumaQuote();
                addJumaQuote.JumaQuoteId = jumaQuote.JumaQuoteId;
                addJumaQuote.JumaQuoteText = jumaQuote.JumaQuoteText;
                addJumaQuote.JumaQuoteLanguage = jumaQuote.JumaQuoteLanguage;
                addJumaQuote.JumaQuoteValidOn = jumaQuote.JumaQuoteValidOn;

                //Default Values
                addJumaQuote.IsActive = true;
                addJumaQuote.CreatedBy = userId;
                addJumaQuote.CreatedOn = DateTime.Now;
                addJumaQuote.UpdatedBy = userId;
                addJumaQuote.UpdatedOn = DateTime.Now;

                result = _unitOfWork.jumaQuoteRepository.AddJumaQuote(addJumaQuote);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateJumaQuote(JumaQuoteDTO jumaQuote, string userId)
        {
            try
            {
                var result = false;
                if (jumaQuote == null)
                    return result;

                var _jumaQuote = _unitOfWork.jumaQuoteRepository.GetJumaQuoteById(jumaQuote.JumaQuoteId);

                if (_jumaQuote == null)
                    return result;

                var updateJumaQuote = new JumaQuote();
                updateJumaQuote.JumaQuoteId = jumaQuote.JumaQuoteId;
                updateJumaQuote.JumaQuoteLanguage = jumaQuote.JumaQuoteLanguage;
                updateJumaQuote.JumaQuoteText = jumaQuote.JumaQuoteText;
                updateJumaQuote.JumaQuoteValidOn = jumaQuote.JumaQuoteValidOn;


                ////Default Values
                updateJumaQuote.IsActive = _jumaQuote.IsActive;
                updateJumaQuote.CreatedBy = _jumaQuote.CreatedBy;
                updateJumaQuote.CreatedOn = _jumaQuote.CreatedOn;
                updateJumaQuote.UpdatedBy = userId;
                updateJumaQuote.UpdatedOn = DateTime.Now;

                result = _unitOfWork.jumaQuoteRepository.UpdateJumaQuote(updateJumaQuote);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteJumaQuote(JumaQuoteDTO jumaQuote, string userId)
        {
            try
            {
                var result = false;
                if (jumaQuote == null)
                    return result;

                var deleteJumaQuote = new JumaQuote();
                deleteJumaQuote.JumaQuoteId = jumaQuote.JumaQuoteId;
                deleteJumaQuote.JumaQuoteText = jumaQuote.JumaQuoteText;
                deleteJumaQuote.JumaQuoteLanguage = jumaQuote.JumaQuoteLanguage;
                deleteJumaQuote.JumaQuoteValidOn = jumaQuote.JumaQuoteValidOn;

                //Default Values
                deleteJumaQuote.IsActive = false;
                deleteJumaQuote.UpdatedBy = userId;
                deleteJumaQuote.UpdatedOn = DateTime.Now;

                result = _unitOfWork.jumaQuoteRepository.DeleteJumaQuote(deleteJumaQuote);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteJumaQuoteById(int jumaQuoteID, string userId)
        {
            try
            {
                var result = false;
                if (jumaQuoteID == 0)
                    return result;

                var jumaQuote = _unitOfWork.jumaQuoteRepository.GetJumaQuoteById(jumaQuoteID);

                if (jumaQuote == null)
                    return result;

                var deleteJumaQuote = new JumaQuote();
                deleteJumaQuote = jumaQuote;
                ////Default Values
                deleteJumaQuote.IsActive = false;
                deleteJumaQuote.UpdatedBy = userId;
                deleteJumaQuote.UpdatedOn = DateTime.Now;

                result = _unitOfWork.jumaQuoteRepository.DeleteJumaQuote(deleteJumaQuote);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<JumaQuoteDTO> GetAllJumaQuotes(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<JumaQuoteDTO>();
                var jumaQuoteList = new List<JumaQuoteDTO>();
                JumaQuoteDTO jumaQuoteResponse;

                var allJumaQuotes = _unitOfWork.jumaQuoteRepository.GetAllJumaQuotes(pageIndex, pageSize);

                if (allJumaQuotes.TotalCount == 0)
                    return response;

                foreach (var jumaQuote in allJumaQuotes.Response)
                {
                    if (jumaQuote.IsActive)
                    {
                        jumaQuoteResponse = new JumaQuoteDTO();
                        jumaQuoteResponse.JumaQuoteId = jumaQuote.JumaQuoteId;
                        jumaQuoteResponse.JumaQuoteText = jumaQuote.JumaQuoteText;
                        jumaQuoteResponse.JumaQuoteLanguage = jumaQuote.JumaQuoteLanguage;
                        jumaQuoteResponse.JumaQuoteValidOn = jumaQuote.JumaQuoteValidOn;
                        jumaQuoteList.Add(jumaQuoteResponse);
                    }
                }
                response.Response = jumaQuoteList;
                response.TotalCount = allJumaQuotes.TotalCount;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JumaQuoteDTO GetJumaQuoteById(int jumaQuoteID)
        {
            try
            {
                JumaQuoteDTO jumaQuoteResponse = new JumaQuoteDTO();
                var jumaQuote = _unitOfWork.jumaQuoteRepository.GetJumaQuoteById(jumaQuoteID);
                if (jumaQuote == null)
                    return null;

                if (jumaQuote.IsActive)
                {
                    jumaQuoteResponse.JumaQuoteId = jumaQuote.JumaQuoteId;
                    jumaQuoteResponse.JumaQuoteLanguage = jumaQuote.JumaQuoteLanguage;
                    jumaQuoteResponse.JumaQuoteText = jumaQuote.JumaQuoteText;
                    jumaQuoteResponse.JumaQuoteValidOn = jumaQuote.JumaQuoteValidOn;
                }
                return jumaQuoteResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JumaQuoteDTO GetJumaQuoteByLang_Date(int languagecode, DateTime utcDateTime)
        {
            try
            {
                JumaQuoteDTO jumaQuoteResponse = new JumaQuoteDTO();
                var jumaQuote = _unitOfWork.jumaQuoteRepository.GetJumaQuoteByLang_Date(languagecode, utcDateTime);
                if (jumaQuote == null)
                    return null;

                if (jumaQuote.IsActive)
                {
                    jumaQuoteResponse.JumaQuoteId = jumaQuote.JumaQuoteId;
                    jumaQuoteResponse.JumaQuoteLanguage = jumaQuote.JumaQuoteLanguage;
                    jumaQuoteResponse.JumaQuoteText = jumaQuote.JumaQuoteText;
                    jumaQuoteResponse.JumaQuoteValidOn = jumaQuote.JumaQuoteValidOn;
                }
                return jumaQuoteResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        #endregion

    }
}