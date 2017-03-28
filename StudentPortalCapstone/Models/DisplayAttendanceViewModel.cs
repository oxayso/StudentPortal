using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentPortalCapstone.Models
{
    public class DisplayAttendanceViewModel
    {
        public List<Attendance> Attendances { get; set; }

        public string CourseName { get; set; }
        public DateTime Date { get; set; }
    }
}