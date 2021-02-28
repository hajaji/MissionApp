using MissionApp.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MissionApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MissionApp.Repository
{
    public class MissionRepository : IMissionRepository
    {
        public MissionContext _missionContext;
        public MissionRepository(MissionContext missionContext)
        {
            _missionContext = missionContext;
        }

        public async Task<bool> AddMission(Mission mission)
        {
            bool result = false;
            try
            {
                await _missionContext.Missions.AddAsync(mission);
                result = await _missionContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                //TODO:add logs and handle exception                
            }

            return result;
        }


        public async Task<Agent> GetAgentByName(string agentName)
        {
            Agent agent = null;
            try
            {
                agent = await _missionContext.Agents.Where(a => a.Name == agentName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                //TODO:add logs and handle exception                
            }
            return agent;
        }

        public async Task<Country> GetCountryByName(string countryName)
        {
            Country country = null;
            try
            {
                country = await _missionContext.Countries.Where(c => c.Name == countryName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                //TODO:add logs and handle exception                
            }
            return country;
        }        

        public async Task<List<Mission>> GetMissions()
        {
            List<Mission> missions = null;
            try
            {
                missions = await _missionContext.Missions.Include(m => m.Agent).Include(m => m.Country).ToListAsync();
            }
            catch (Exception ex)
            {
                //TODO:add logs and handle exception                
            }
            return missions;
        }

        public async Task<bool> IsMissionExist(string agentName, string date)
        {
            Mission mission = null;
            try
            {
                DateTime missionDate = DateTime.Parse(date);
                mission = await _missionContext.Missions.Include(m => m.Agent).FirstOrDefaultAsync(m => m.Agent.Name == agentName && m.Date == missionDate);
            }
            catch (Exception ex)
            {
                //TODO:add logs and handle exception
            }

            return mission != null;
        }
    }
}
