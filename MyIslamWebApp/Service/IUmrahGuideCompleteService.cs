using MyIslamWebApp.DataTransferObjects.UmrahGuideComplete;
using System.Collections.Generic;

namespace MyIslamWebApp.Service
{
    public interface IUmrahGuideCompleteService
    {
        //UmrahGuideComplete CRUD Operations
        IEnumerable<UmrahGuideCompleteDTO> GetAllUmrahGuideComplete();
        IEnumerable<UmrahGuideCompleteDTO> GetAllUmrahGuideCompleteByUser(string umrahGuideCompleteUser);
        bool AddUmrahGuideComplete(UmrahGuideCompleteDTO UmrahGuideComplete, string userId);
        bool DeleteUmrahGuideComplete(int umrahGuideCompleteId);
    }
}