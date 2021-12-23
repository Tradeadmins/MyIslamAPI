using MyIslamWebApp.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyIslamWebApp.Models
{
    public class PrayerRequest : BaseModel
    {
        /// <summary>
        /// Id of the PrayerRequest
        /// </summary>
        [Key]
        public int PrayerRequestId { get; set; }
        /// <summary>
        /// PrayerRequest's Text
        /// </summary>
        [Required]
        public string PrayerRequestText { get; set; }
    }
}