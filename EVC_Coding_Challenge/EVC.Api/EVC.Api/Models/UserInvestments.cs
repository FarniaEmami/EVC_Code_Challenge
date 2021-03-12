using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EVC.Api.Models
{
    public class UserInvestments
    {
        [Key]
        public int userInvestmentsId { get; set; }
        public int userID { get; set; }
        public int projectID { get; set; }

        [Range(100, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int investmentAmount { get; set; }
    }
}

