using MyIslamWebApp.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyIslamWebApp.Enums;

namespace MyIslamWebApp.Models
{
    public class MyEvent : BaseModel
    {
        /// <summary>
        /// Id of the MyEvent
        /// </summary>
        [Key]
        public int MyEventId { get; set; }

        /// <summary>
        /// MyEvent's Category
        /// </summary>
        public EventCategory MyEventCategory { get; set; }

        /// <summary>
        /// MyEvent's Name
        /// </summary>
        public string MyEventName { get; set; }

        /// <summary>
        /// MyEvent's Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// MyEvent's City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// MyEvent's Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// MyEvent's MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// MyEvent's Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// MyEvent's MyEvent Latitude
        /// </summary>
        public double MyEventLatitude { get; set; }

        /// <summary>
        /// MyEvent's Description
        /// </summary>
        public double MyEventLongitude { get; set; }

        /// <summary>
        /// MyEvent's MyEvent Minor
        /// </summary>
        public bool MyEventMinor { get; set; }

        /// <summary>
        /// MyEvent's MyEvent Start Date
        /// </summary>
        public DateTime MyEventStartDate { get; set; }

        /// <summary>
        /// MyEvent's MyEvent End Date
        /// </summary>
        public DateTime MyEventEndDate { get; set; }

        ///// <summary>
        ///// MyEvent's Distance
        ///// </summary>
        //[NotMapped]
        //public double? Distance { get; set; }
    }
}