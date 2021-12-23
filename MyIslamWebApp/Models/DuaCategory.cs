using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class DuaCategory : BaseModel
    {
        /// <summary>
        /// Id of the DuaCategory
        /// </summary>
        [Key]
        public int DuaCategoryId { get; set; }
        /// <summary>
        /// Dua Category Name
        /// </summary>
        public string DuaCategoryName { get; set; }
    }
}