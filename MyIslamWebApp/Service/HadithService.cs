using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.Hadith;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class HadithService : IHadithService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public HadithService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public HadithService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Hadith Methods
        public bool AddHadith(HadithDTO hadith, string userId)
        {
            try
            {
                var result = false;

                if (hadith == null)
                    return result;

                var addHadith = new Hadith();
                addHadith.HadithId = hadith.HadithId;
                addHadith.HadithArabicText = hadith.HadithArabicText;
                addHadith.HadithEnglishText = hadith.HadithEnglishText;
                addHadith.HadithTurkeyText = hadith.HadithTurkeyText;
                addHadith.HadithMalayText = hadith.HadithMalayText;
                addHadith.HadithPronunciationText = hadith.HadithPronunciationText;
                //Default Values
                addHadith.IsActive = true;
                addHadith.CreatedBy = userId;
                addHadith.CreatedOn = DateTime.Now;
                addHadith.UpdatedBy = userId;
                addHadith.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hadithRepository.AddHadith(addHadith);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateHadith(HadithDTO hadith, string userId)
        {
            try
            {
                var result = false;
                if (hadith == null)
                    return result;

                var _hadith = _unitOfWork.hadithRepository.GetHadithById(hadith.HadithId);

                if (_hadith == null)
                    return result;

                var updateHadith = new Hadith();
                updateHadith.HadithId = hadith.HadithId;                
                updateHadith.HadithArabicText = hadith.HadithArabicText;
                updateHadith.HadithEnglishText = hadith.HadithEnglishText;
                updateHadith.HadithTurkeyText = hadith.HadithTurkeyText;
                updateHadith.HadithMalayText = hadith.HadithMalayText;
                updateHadith.HadithPronunciationText = hadith.HadithPronunciationText;
                ////Default Values
                updateHadith.IsActive = _hadith.IsActive;
                updateHadith.CreatedBy = _hadith.CreatedBy;
                updateHadith.CreatedOn = _hadith.CreatedOn;
                updateHadith.UpdatedBy = userId;
                updateHadith.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hadithRepository.UpdateHadith(updateHadith);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteHadith(HadithDTO hadith, string userId)
        {
            try
            {
                var result = false;
                if (hadith == null)
                    return result;

                var deleteHadith = new Hadith();
                deleteHadith.HadithId = hadith.HadithId;
              
                deleteHadith.HadithArabicText = hadith.HadithArabicText;
                deleteHadith.HadithEnglishText = hadith.HadithEnglishText;
                deleteHadith.HadithTurkeyText = hadith.HadithTurkeyText;
                deleteHadith.HadithMalayText = hadith.HadithMalayText;
                deleteHadith.HadithPronunciationText = hadith.HadithPronunciationText;

                deleteHadith.IsActive = false;
                deleteHadith.UpdatedBy = userId;
                deleteHadith.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hadithRepository.DeleteHadith(deleteHadith);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteHadithById(int hadithID, string userId)
        {
            try
            {
                var result = false;
                if (hadithID == 0)
                    return result;

                var hadith = _unitOfWork.hadithRepository.GetHadithById(hadithID);

                if (hadith == null)
                    return result;

                var deleteHadith = new Hadith();
                deleteHadith = hadith;

                ////Default Values
                deleteHadith.IsActive = false;
                deleteHadith.UpdatedBy = userId;
                deleteHadith.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hadithRepository.DeleteHadith(deleteHadith);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HadithDTO GetHadithById(int hadithID)
        {
            try
            {
                HadithDTO hadithResponse = new HadithDTO();
                var hadith = _unitOfWork.hadithRepository.GetHadithById(hadithID);
                if (hadith == null)
                    return null;

                if (hadith.IsActive)
                {
                    hadithResponse.HadithId = hadith.HadithId;
                   
                    hadithResponse.HadithArabicText = hadith.HadithArabicText;
                    hadithResponse.HadithEnglishText = hadith.HadithEnglishText;
                    hadithResponse.HadithTurkeyText = hadith.HadithTurkeyText;
                    hadithResponse.HadithMalayText = hadith.HadithMalayText;
                    hadithResponse.HadithPronunciationText = hadith.HadithPronunciationText;
                }
                return hadithResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<HadithDTO> GetAllHadiths(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<HadithDTO>();
                var hadithList = new List<HadithDTO>();
                HadithDTO hadithResponse;              

                var allHadiths = _unitOfWork.hadithRepository.GetAllHadiths(pageIndex, pageSize);


                if (allHadiths.TotalCount == 0)
                    return response;

                foreach (var hadith in allHadiths.Response)
                {
                    if (hadith.IsActive)
                    {
                        hadithResponse = new HadithDTO();
                        hadithResponse.HadithId = hadith.HadithId;                       
                        hadithResponse.HadithArabicText = hadith.HadithArabicText;
                        hadithResponse.HadithEnglishText = hadith.HadithEnglishText;
                        hadithResponse.HadithTurkeyText = hadith.HadithTurkeyText;
                        hadithResponse.HadithMalayText = hadith.HadithMalayText;
                        hadithResponse.HadithPronunciationText = hadith.HadithPronunciationText;
                        hadithList.Add(hadithResponse);
                    }
                }
                response.Response = hadithList;
                response.TotalCount = allHadiths.TotalCount;
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