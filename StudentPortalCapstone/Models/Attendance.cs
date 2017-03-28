using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentPortalCapstone.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Roster")]
        public int RosterId { get; set; }
        public Roster Roster { get; set; }
        [DisplayName("Status")]
        public bool isPresent { get; set; }
    }
}