using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Semester_Projekt.Models
{
    public class Ratings
    {
        public int Rating_id { get; set; }
        public int User_Id { get; set; }

        public int Post_Id { get; set; }

        public String? Rating_comment { get; set; }
        public User? User { get; set; }

        public int Rating { get; set; } //min 1 max 5

        public int? Likes { get; set; }
        //public List<User> User_Likes { get; set; }

    }
}
