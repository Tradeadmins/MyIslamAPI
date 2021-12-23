using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.UmrahGuide;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IUmrahGuideService
    {
        //UmrahGuide CRUD Operations
        ResultDTO<UmrahGuideListDTO> GetAllUmrahGuides(int pageIndex, int pageSize, string userId);
        UmrahGuideDTO GetUmrahGuideById(int umrahGuideId);
        bool DeleteUmrahGuide(UmrahGuideDTO dua, string userId);
        bool AddUmrahGuide(UmrahGuideDTO umrahGuide, string userId);
        bool DeleteUmrahGuideById(int umrahGuideId, string userId);
        bool UpdateUmrahGuide(UmrahGuideDTO umrahGuide, string userId);
    }
}