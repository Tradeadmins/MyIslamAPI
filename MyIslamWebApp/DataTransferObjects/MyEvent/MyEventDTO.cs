using System;
using System.ComponentModel.DataAnnotations;
using MyIslamWebApp.Enums;

namespace MyIslamWebApp.DataTransferObjects.MyEvent
{
    public class MyEventDTO
    {
        public int MyEventId { get; set; }
        [Required]
        public EventCategory MyEventCategory { get; set; }
        [Required]
        public string MyEventName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double MyEventLatitude { get; set; }
        [Required]
        public double MyEventLongitude { get; set; }
        [Required]
        public bool MyEventMinor { get; set; }
        [Required]
        public DateTime MyEventStartDate { get; set; }
        [Required]
        public DateTime MyEventEndDate { get; set; }
        public double? Distance { get; set; }
    }
}