using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.Hadiths
{
    public interface IHadithRepository : IRepository<Hadith>
    {
        bool AddHadith(Hadith hadith);
        bool DeleteHadith(Hadith hadith);
        bool DeleteHadithById(int hadithId);
        bool UpdateHadith(Hadith hadith);
        Hadith GetHadithById(int hadithId);
        Result<Hadith> GetAllHadiths(int pageIndex, int pageSize);
    }
}