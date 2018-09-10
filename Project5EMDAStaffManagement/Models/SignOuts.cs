using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project5EMDAStaffManagement.Models
{
    public class SignOuts
    {
        [Key]
        public int Id { get; set; }
        public Staff Staff { get; set; }
        public DateTime Day { get; set; }
        public DateTime TimeOut { get; set; }
        public Reasons Reason { get; set; }
        public string HoursIn { get; set; }
    }
}
