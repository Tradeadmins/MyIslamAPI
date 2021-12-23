using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.Donation
{
    public class DonationDTO
    {
        public int DonationId { get; set; }
        public int DonationCategoryId { get; set; }
        public decimal DonationAmount { get; set; }
        public decimal DonationLocalAmount { get; set; }
        public string DonationLocalCurrencyType { get; set; }
    }
}