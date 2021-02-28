using Microsoft.EntityFrameworkCore;
using MissionApp.Domain;

namespace MissionApp.Data
{
    public class MissionContext : DbContext
    {
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<AppSetting> AppSettings { get; set; }

        public MissionContext(DbContextOptions<MissionContext> options) : base(options)
        {
            
        }
    }
}
