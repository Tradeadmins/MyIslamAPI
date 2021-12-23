using MyIslamWebApp.DataTransferObjects.UmrahTaskComplete;
using System.Collections.Generic;

namespace MyIslamWebApp.Service
{
    public interface IUmrahTaskCompleteService
    {
        //UmrahTaskComplete CRUD Operations
        IEnumerable<UmrahTaskCompleteDTO> GetAllUmrahTaskComplete();
        IEnumerable<UmrahTaskCompleteDTO> GetAllUmrahTaskCompleteByUser(string umrahTaskCompleteUser);
        bool AddUmrahTaskComplete(UmrahTaskCompleteDTO UmrahTaskComplete, string userId);
        bool DeleteUmrahTaskComplete(int umrahTaskCompleteId);
    }
}