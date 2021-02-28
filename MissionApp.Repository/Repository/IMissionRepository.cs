using MissionApp.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionApp.Repository
{
    public interface IMissionRepository
    {
        Task<bool> AddMission(Mission mission);        
        Task<bool> IsMissionExist(string agentName, string date);
        Task<Agent> GetAgentByName(string agentName);
        Task<Country> GetCountryByName(string countryName);        
        Task<List<Mission>> GetMissions();
    }
}
