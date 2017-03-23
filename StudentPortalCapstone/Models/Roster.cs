using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StudentPortalCapstone.Models
{
    public class Roster
    {
        public int Id { get; set; }
        [DisplayName("Class Name")]
        public string ClassName { get; set; }

    }
}