using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.QuranTranslate;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class QuranTranslateService : IQuranTranslateService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public QuranTranslateService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public QuranTranslateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region QuranTranslate Methods
        public bool AddQuranTranslate(QuranTranslateDTO quranTranslate, string userId)
        {
            try
            {
                var result = false;

                if (quranTranslate == null)
                    return result;

                var addQuranTranslate = new QuranTranslate();
                addQuranTranslate.QuranTranslateId = quranTranslate.QuranTranslateId;
                addQuranTranslate.QuranTranslateLanguage = quranTranslate.QuranTranslateLanguage;
                addQuranTranslate.QuranTranslateBy = quranTranslate.QuranTranslateBy;
                addQuranTranslate.QuranTranslateUrl = quranTranslate.QuranTranslateUrl;

                //Default Values
                addQuranTranslate.IsActive = true;
                addQuranTranslate.CreatedBy = userId;
                addQuranTranslate.CreatedOn = DateTime.Now;
                addQuranTranslate.UpdatedBy = userId;
                addQuranTranslate.UpdatedOn = DateTime.Now;

                result = _unitOfWork.quranTranslateRepository.AddQuranTranslate(addQuranTranslate);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateQuranTranslate(QuranTranslateDTO quranTranslate, string userId)
        {
            try
            {
                var result = false;
                if (quranTranslate == null)
                    return result;

                var _quranTranslate = _unitOfWork.quranTranslateRepository.GetQuranTranslateById(quranTranslate.QuranTranslateId);

                if (_quranTranslate == null)
                    return result;

                var updateQuranTranslate = new QuranTranslate();
             
                updateQuranTranslate.QuranTranslateId = quranTranslate.QuranTranslateId;
                updateQuranTranslate.QuranTranslateLanguage = quranTranslate.QuranTranslateLanguage;
                updateQuranTranslate.QuranTranslateBy = quranTranslate.QuranTranslateBy;
                updateQuranTranslate.QuranTranslateUrl = quranTranslate.QuranTranslateUrl;

                ////Default Values
                updateQuranTranslate.IsActive = _quranTranslate.IsActive;
                updateQuranTranslate.CreatedBy = _quranTranslate.CreatedBy;
                updateQuranTranslate.CreatedOn = _quranTranslate.CreatedOn;
                updateQuranTranslate.UpdatedBy = userId;
                updateQuranTranslate.UpdatedOn = DateTime.Now;

                result = _unitOfWork.quranTranslateRepository.UpdateQuranTranslate(updateQuranTranslate);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteQuranTranslate(QuranTranslateDTO quranTranslate, string userId)
        {
            try
            {
                var result = false;
                if (quranTranslate == null)
                    return result;

                var deleteQuranTranslate = new QuranTranslate();
                deleteQuranTranslate.QuranTranslateId = quranTranslate.QuranTranslateId;
                deleteQuranTranslate.QuranTranslateLanguage = quranTranslate.QuranTranslateLanguage;
                deleteQuranTranslate.QuranTranslateBy = quranTranslate.QuranTranslateBy;
                deleteQuranTranslate.QuranTranslateUrl = quranTranslate.QuranTranslateUrl;

                deleteQuranTranslate.IsActive = false;
                deleteQuranTranslate.UpdatedBy = userId;
                deleteQuranTranslate.UpdatedOn = DateTime.Now;

                result = _unitOfWork.quranTranslateRepository.DeleteQuranTranslate(deleteQuranTranslate);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteQuranTranslateById(int quranTranslateID, string userId)
        {
            try
            {
                var result = false;
                if (quranTranslateID == 0)
                    return result;

                var quranTranslate = _unitOfWork.quranTranslateRepository.GetQuranTranslateById(quranTranslateID);

                if (quranTranslate == null)
                    return result;

                var deleteQuranTranslate = new QuranTranslate();
                deleteQuranTranslate = quranTranslate;

                ////Default Values
                deleteQuranTranslate.IsActive = false;
                deleteQuranTranslate.UpdatedBy = userId;
                deleteQuranTranslate.UpdatedOn = DateTime.Now;

                result = _unitOfWork.quranTranslateRepository.DeleteQuranTranslate(deleteQuranTranslate);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public QuranTranslateDTO GetQuranTranslateById(int quranTranslateID)
        {
            try
            {
                QuranTranslateDTO quranTranslateResponse = new QuranTranslateDTO();
                var quranTranslate = _unitOfWork.quranTranslateRepository.GetQuranTranslateById(quranTranslateID);
                if (quranTranslate == null)
                    return null;

                if (quranTranslate.IsActive)
                {
                    quranTranslateResponse.QuranTranslateId = quranTranslate.QuranTranslateId;
                    quranTranslateResponse.QuranTranslateLanguage = quranTranslate.QuranTranslateLanguage;
                    quranTranslateResponse.QuranTranslateBy = quranTranslate.QuranTranslateBy;
                    quranTranslateResponse.QuranTranslateUrl = quranTranslate.QuranTranslateUrl;
                }
                return quranTranslateResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<QuranTranslateDTO> GetAllQuranTranslate()
        {
            try
            {
                var response = new List<QuranTranslateDTO>();
                QuranTranslateDTO quranTranslateResponse;
                var allQuranTranslates = _unitOfWork.quranTranslateRepository.GetAllQuranTranslates();

                if (allQuranTranslates == null)
                    return response;

                foreach (var quranTranslate in allQuranTranslates)
                {
                    if (quranTranslate.IsActive)
                    {
                        quranTranslateResponse = new QuranTranslateDTO();
                        quranTranslateResponse.QuranTranslateId = quranTranslate.QuranTranslateId;
                        quranTranslateResponse.QuranTranslateLanguage = quranTranslate.QuranTranslateLanguage;
                        quranTranslateResponse.QuranTranslateBy = quranTranslate.QuranTranslateBy;
                        quranTranslateResponse.QuranTranslateUrl = quranTranslate.QuranTranslateUrl;
                        response.Add(quranTranslateResponse);
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