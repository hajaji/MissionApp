using System;

namespace MissionApp.Models
{
    public class MissionResponse
    {        
        public string Agent { get; set; }        
        public string Country { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
    }
}
