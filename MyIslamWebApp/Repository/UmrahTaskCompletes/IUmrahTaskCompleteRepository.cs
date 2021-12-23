using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System.Collections.Generic;

namespace MyIslamWebApp.Repository.UmrahTaskCompletes
{
    public interface IUmrahTaskCompleteRepository : IRepository<UmrahTaskComplete>
    {
        IEnumerable<UmrahTaskComplete> GetAllUmrahTaskComplete();
        IEnumerable<UmrahTaskComplete> GetAllUmrahTaskCompleteByUser(string umrahTaskCompleteUser);
        bool AddUmrahTaskComplete(UmrahTaskComplete UmrahTaskComplete);
        bool DeleteUmrahTaskComplete(int umrahTaskCompleteId);
        UmrahTaskComplete GetUmrahTaskCompleteById(int makeDuaId);
    }
}