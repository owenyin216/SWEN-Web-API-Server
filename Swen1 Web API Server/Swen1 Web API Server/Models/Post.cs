using System;
using System.Collections.Generic;
using System.Text;

namespace Semester_Projekt.Models
{
    public class Post
    {
        public int Post_id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public string Body { get; set; }
        public MediaEntry? Media { get; set; } //the link to the media entry
        public int Poster_Id {  get; set; }
        public User? Poster { get; set; }
        //public Status? Status { get; set; }
        public DateTime Created_at { get; set; }

        public List<Ratings>? Ratings { get; set; }

        //count fav
    }
}
