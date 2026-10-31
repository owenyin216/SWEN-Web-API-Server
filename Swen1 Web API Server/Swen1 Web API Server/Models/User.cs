using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Semester_Projekt.Models
{
    public class User
    {
       public int User_id { get ; set; }

       [JsonIgnore]
       public string Password { get; set; } = string.Empty;

        //to do token
       public String Username  {  get; set; }
       public int Roles_Id { get; set; }
       public Roles? Roles { get; set; }
       public DateTime Created_At { get; set; }
       public String? Email { get; set; }
       public bool Email_Certified { get; set; } 
       public DateOnly Birthdate { get; set; }
       public bool Ageverified { get; set; }

       public List<Post>? Favourites { get; set; }
    }
}
