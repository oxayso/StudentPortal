using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentPortalCapstone.Models
{
    public class ReportCard
    {
        public int Id  { get; set; }
        [DisplayName("Student Grade")]
        public double StudentPts { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Assignments")]
        public int AssignmentsId { get; set; }
        public Assignments Assignments { get; set; }
        public bool HasBeenGraded { get; set; }
    
    }
}