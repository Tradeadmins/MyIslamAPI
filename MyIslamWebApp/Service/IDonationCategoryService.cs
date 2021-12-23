using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.DonationCategory;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IDonationCategoryService
    {
        //DonationCategory CRUD Operations
        IEnumerable<DonationCategoryDTO> GetAllDonationCategory();
        DonationCategoryDTO GetDonationCategoryById(int donationCategoryId);
        bool DeleteDonationCategory(DonationCategoryDTO donation, string userId);
        bool AddDonationCategory(DonationCategoryDTO donationCategory, string userId);
        bool DeleteDonationCategoryById(int donationCategoryId, string userId);
        bool UpdateDonationCategory(DonationCategoryDTO donationCategory, string userId);
    }
}