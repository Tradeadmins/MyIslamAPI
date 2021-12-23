using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.QuranTranslate;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IQuranTranslateService
    {
        //QuranTranslate CRUD Operations
        IEnumerable<QuranTranslateDTO> GetAllQuranTranslate();
        QuranTranslateDTO GetQuranTranslateById(int quranTranslateId);
        bool DeleteQuranTranslate(QuranTranslateDTO donation, string userId);
        bool AddQuranTranslate(QuranTranslateDTO quranTranslate, string userId);
        bool DeleteQuranTranslateById(int quranTranslateId, string userId);
        bool UpdateQuranTranslate(QuranTranslateDTO quranTranslate, string userId);
    }
}