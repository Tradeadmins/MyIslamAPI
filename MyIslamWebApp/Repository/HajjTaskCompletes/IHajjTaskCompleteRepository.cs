using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System.Collections.Generic;

namespace MyIslamWebApp.Repository.HajjTaskCompletes
{
    public interface IHajjTaskCompleteRepository : IRepository<HajjTaskComplete>
    {
        IEnumerable<HajjTaskComplete> GetAllHajjTaskComplete();
        IEnumerable<HajjTaskComplete> GetAllHajjTaskCompleteByUser(string hajjTaskCompleteUser);
        bool AddHajjTaskComplete(HajjTaskComplete HajjTaskComplete);
        bool DeleteHajjTaskComplete(int hajjTaskCompleteId);
        HajjTaskComplete GetHajjTaskCompleteById(int hajjTaskCompleteId);
    }
}