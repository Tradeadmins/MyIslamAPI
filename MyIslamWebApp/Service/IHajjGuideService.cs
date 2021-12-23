using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.HajjGuide;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IHajjGuideService
    {
        //HajjGuide CRUD Operations
        ResultDTO<HajjGuideListDTO> GetAllHajjGuides(int pageIndex, int pageSize, string userId);
        HajjGuideDTO GetHajjGuideById(int hajjGuideId);
        bool DeleteHajjGuide(HajjGuideDTO dua, string userId);
        bool AddHajjGuide(HajjGuideDTO hajjGuide, string userId);
        bool DeleteHajjGuideById(int hajjGuideId, string userId);
        bool UpdateHajjGuide(HajjGuideDTO hajjGuide, string userId);
    }
}