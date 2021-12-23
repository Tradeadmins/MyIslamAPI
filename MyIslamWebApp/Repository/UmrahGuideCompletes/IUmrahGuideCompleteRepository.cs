using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System.Collections.Generic;

namespace MyIslamWebApp.Repository.UmrahGuideCompletes
{
    public interface IUmrahGuideCompleteRepository : IRepository<UmrahGuideComplete>
    {
        IEnumerable<UmrahGuideComplete> GetAllUmrahGuideComplete();
        IEnumerable<UmrahGuideComplete> GetAllUmrahGuideCompleteByUser(string umrahGuideCompleteUser);
        bool AddUmrahGuideComplete(UmrahGuideComplete UmrahGuideComplete);
        bool DeleteUmrahGuideComplete(int umrahGuideCompleteId);
        UmrahGuideComplete GetUmrahGuideCompleteById(int makeDuaId);
    }
}