using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models.Base
{
    /// <summary>
    /// To provide a base layout of properties used in most of the Models; thus avoids code duplications
    /// </summary>
    public abstract class BaseModel
    {
        //public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}