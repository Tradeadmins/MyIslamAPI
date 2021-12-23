using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.QuranTranslates
{
    public interface IQuranTranslateRepository : IRepository<QuranTranslate>
    {
        bool AddQuranTranslate(QuranTranslate quranTranslate);
        bool DeleteQuranTranslate(QuranTranslate quranTranslate);
        bool DeleteQuranTranslateById(int quranTranslateId);
        bool UpdateQuranTranslate(QuranTranslate quranTranslate);
        QuranTranslate GetQuranTranslateById(int quranTranslateId);
        IEnumerable<QuranTranslate> GetAllQuranTranslates();
    }
}