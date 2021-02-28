using System;

namespace MissionApp.Domain
{
    public class Mission
    {
        public int Id { get; set; }        
        public Agent Agent { get; set; }        
        public Country Country { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
    }
}
