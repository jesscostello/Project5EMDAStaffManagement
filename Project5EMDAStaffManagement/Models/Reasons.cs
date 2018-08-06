using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Project5EMDAStaffManagement.Models
{
    public class Reasons
    {
        [Key]
        public int Id { get; set; }
        public string Reason { get; set; }
        public int ReasonCount { get; set; }
    }
}
