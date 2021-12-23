using MyIslamWebApp.DataTransferObjects.Donation;
using System.Collections.Generic;

namespace MyIslamWebApp.Service
{
    public interface IDonationService
    {
        //Donation CRUD Operations
        decimal GetAllDonationAmounts();
        DonationDTO GetDonationById(int donationId);
        bool AddDonation(DonationDTO donation, string userId);
        bool DeleteDonationById(int donationID, string userId);
        bool UpdateDonation(DonationDTO donation, string userId);
        bool DeleteDonation(DonationDTO donation, string userId);
        decimal GetAllDonationByUserId(string userId);
    }
}