using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MissionApp.API.Models
{
    public class MissionRequest
    {        
        public string Agent { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
    }
}
