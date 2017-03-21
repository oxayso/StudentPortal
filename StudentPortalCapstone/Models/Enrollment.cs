using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentPortalCapstone.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Roster")]
        public int RosterId { get; set; }
        public Roster Roster { get; set; }

    }
}