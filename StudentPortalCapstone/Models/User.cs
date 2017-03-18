using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentPortalCapstone.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }


        public enum RoleList
        {
          Teacher = 1,
          Student 
        }

        public RoleList Roles { get; set; }
    }
}