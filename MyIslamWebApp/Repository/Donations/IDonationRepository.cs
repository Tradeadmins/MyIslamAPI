using MyIslamWebApp.DataTransferObjects.Donation;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System.Collections.Generic;

namespace MyIslamWebApp.Repository.Donations
{

    public interface IDonationRepository : IRepository<Donation>
    {
        decimal GetAllDonationAmounts();
        Donation GetDonationById(int donationId);
        bool AddDonation(Donation donation);
        bool DeleteDonation(Donation donation);
        bool DeleteDonationById(int donationId);
        bool UpdateDonation(Donation donation);
        decimal GetAllDonationByUserId(string userId);
    }
}