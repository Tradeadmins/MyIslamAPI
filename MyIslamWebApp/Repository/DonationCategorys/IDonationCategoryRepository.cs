using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.DonationCategorys
{
    public interface IDonationCategoryRepository : IRepository<DonationCategory>
    {
        bool AddDonationCategory(DonationCategory donation);
        bool DeleteDonationCategory(DonationCategory donation);
        bool DeleteDonationCategoryById(int donationId);
        bool UpdateDonationCategory(DonationCategory donation);
        DonationCategory GetDonationCategoryById(int donationId);
        IEnumerable<DonationCategory> GetAllDonationCategorys();
    }
}