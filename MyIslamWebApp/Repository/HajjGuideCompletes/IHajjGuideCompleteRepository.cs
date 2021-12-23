using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System.Collections.Generic;

namespace MyIslamWebApp.Repository.HajjGuideCompletes
{
    public interface IHajjGuideCompleteRepository : IRepository<HajjGuideComplete>
    {
        IEnumerable<HajjGuideComplete> GetAllHajjGuideComplete();
        IEnumerable<HajjGuideComplete> GetAllHajjGuideCompleteByUser(string hajjGuideCompleteUser);
        bool AddHajjGuideComplete(HajjGuideComplete HajjGuideComplete);
        bool DeleteHajjGuideComplete(int hajjTaskCompleteId);
        HajjGuideComplete GetHajjGuideCompleteById(int makeDuaId);
    }
}