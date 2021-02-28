using MissionApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MissionApp.Models;

namespace MissionApp.Service
{
    public interface IMissionService
    {   
        Task<bool> AddMission(MissionRequest missionRequest);
        Task<(string,int)> GetCountryByIsolation();
        Task<(MissionResponse, double)> FindClosestMission(string address);
    }
}
