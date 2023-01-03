using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApplication.Models
{
  
    public class CaptionDTO
    {
        public string FormName { get; set; }
        public string Caption { get; set; }
        public string Field { get; set; }
    }
}
