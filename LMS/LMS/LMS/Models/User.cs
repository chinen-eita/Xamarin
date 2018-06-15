using System;
using System.Collections.Generic;
using System.Text;
using Realms;

namespace LMS.Models
{
    public class User : RealmObject
    {
        [PrimaryKey]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
