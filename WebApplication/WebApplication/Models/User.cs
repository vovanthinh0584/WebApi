using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string BusinessUnitID { get; set; }
        public string TOKEN { get; set; }
        public string Message { get; set; }

        public string Language { get; set; }

        public DateTime SessionExpires { get; set; }
        

        public List<string> Permissions { get; set; }
    }
}
