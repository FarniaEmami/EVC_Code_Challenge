using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace EVC.Api.Models
{
    public class Users
    {
        [Key]
        public int userID { get; set; }
        public string userName { get; set; }
    }
}
