using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using System.Collections.Generic;
using MyIslamWebApp.DataTransferObjects.Result;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Service
{
    public interface IPrayerRequestService
    {
        //PrayerRequest CRUD Operations
        ResultDTO<PrayerRequestListDTO> GetAllPrayerRequests(int pageIndex, int pageSize, string userId);
        PrayerRequestDTO GetPrayerRequestById(int prayerRequestId);
        Result<PrayerRequestListDTO> GetAllPrayerRequestByUserId(string prayerRequestUserId);
        bool AddPrayerRequest(PrayerRequestDTO prayerRequest, string userId);
        bool DeletePrayerRequestById(int prayerRequestId, string userId);
        bool UpdatePrayerRequest(PrayerRequestDTO prayerRequest, string userId);
    }
}