using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EVC.Api.Models
{
    public class Projects
    {
        [Key]
        public int projectId { get; set; }
        public string projectName { get; set; }
        public int totalFund { get; set; }
        public int remainingFund { get; set; }
        public bool active { get; set; }
    }
}
