using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentPortalCapstone.Models
{
    public class Assignments
    {
        public int Id { get; set; }
        [DisplayName("Assignment Name")]
        public string AssignmentName { get; set; }
        [ForeignKey("Roster")]
        public int RosterId { get; set; }
        public Roster Roster { get; set; }

        [DisplayName("Maximum Points")]
        public double MaxPts{ get; set; }
    }
}