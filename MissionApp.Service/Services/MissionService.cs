using MissionApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MissionApp.Repository;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using MissionApp.Service.Models;
using MissionApp.Models;
using AutoMapper;

namespace MissionApp.Service
{
    public class MissionService : IMissionService
    {
        IMissionRepository _missionRepository;
        IAppSettingRepository _appSettingRepository;
        private readonly IMapper _mapper;
        public MissionService(IMissionRepository missionRepository, IAppSettingRepository appSettingRepository,IMapper mapper)
        {
            _missionRepository = missionRepository;
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Add mission and the correspondent agent and country
        /// </summary>
        /// <param name="mission"></param>
        /// <returns></returns>
        public async Task<bool> AddMission(MissionRequest missionRequest)
        {
            Mission mission = _mapper.Map<Mission>(missionRequest);

            //if agent already eixst we will not add another one
            Agent agent = await _missionRepository.GetAgentByName(mission.Agent.Name);
            mission.Agent = agent ?? mission.Agent;

            //if country already eixst we will not add another one
            Country country = await _missionRepository.GetCountryByName(mission.Country.Name);
            mission.Country = country ?? mission.Country;

            return await _missionRepository.AddMission(mission);
        }

        /// <summary>
        /// Get most isoleted country
        /// </summary>
        /// <returns></returns>
        public async Task<(string, int)> GetCountryByIsolation()
        {
            List<Mission> missions = await _missionRepository.GetMissions();

            Dictionary<int, List<string>> agentCountries = BuildAgentCountryDic(missions);

            //Get all agents with one mission (hence one country)
            Dictionary<string, int> groupedAgents = agentCountries.Where(a => a.Value.Count() == 1)
                .GroupBy(a => a.Value.First()) //Group the agent by country
                .ToDictionary(g => g.Key, g => g.ToList().Count());

            KeyValuePair<string, int> result = groupedAgents.FirstOrDefault(x => x.Value == groupedAgents.Values.Max());

            return (result.Key, result.Value);
        }


        /// <summary>
        /// Find the closest missing to a given address
        /// </summary>
        /// <param name="originAddress"></param>
        /// <returns></returns>
        public async Task<(MissionResponse, double)> FindClosestMission(string originAddress)
        {
            double smallestDistance = 0;
            Mission mission = null;

            //Get all missions
            List<Mission> missions = await _missionRepository.GetMissions();

            //Create Task<HttpResponseMessage> for each API call
            List<Task<HttpResponseMessage>> responseTasks = await GetResponseTasks(missions, originAddress);

            try
            {
                //Run API calls in parallel
                HttpResponseMessage[] responses = await Task.WhenAll(responseTasks);

                //Create Task<string> for each response.Content
                List<Task<string>> contentTasks = GetContentTasks(responses);

                //Get response.Content in parallel
                string[] contents = await Task.WhenAll(contentTasks);

                //Get the closest mission and the distance
                (mission, smallestDistance) = GetClosestMission(missions, contents);

            }
            catch (Exception ex)
            {
                //TODO:add logs here                
            }

            MissionResponse missionResponse = _mapper.Map<MissionResponse>(mission);

            return (missionResponse, smallestDistance);
        }


        #region Private methods
        /// <summary>
        /// Extract the json data from Google API
        /// </summary>
        /// <param name="missions"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        private (Mission, double) GetClosestMission(List<Mission> missions, string[] contents)
        {
            double smallestDistance = double.MaxValue;
            Mission mission = null;

            //The first element is the origin address that we got from the request
            Coordinate originCoordinate = GetLocation(JObject.Parse(contents[0]));

            //We start the loop from i=1 because we already took the first location-contents[0] 
            for (int i = 1; i < contents.Length; i++)
            {
                dynamic googleResponse = JObject.Parse(contents[i]);
                string status = googleResponse["status"].ToString();
                Coordinate destinationCoordinate = GetLocation(googleResponse);
                double distance = Geolocation.GeoCalculator.GetDistance(originCoordinate.Lat,originCoordinate.Lng,destinationCoordinate.Lat,destinationCoordinate.Lng);             
                
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    mission = missions[i - 1];
                }
            }

            return (mission, smallestDistance);
        }


        /// <summary>
        /// Parse result from google
        /// </summary>
        /// <param name="googleResponse"></param>
        /// <returns></returns>
        private Models.Coordinate GetLocation(dynamic googleResponse)
        {

            Models.Coordinate location = null;
            string status = googleResponse["status"].ToString();

            if (status == "OK")
            {
                location = JsonConvert.DeserializeObject<Models.Coordinate>(googleResponse["results"].First["geometry"]["location"].ToString());
            }
            else
            {
                //TODO:thow exception and write log
            }

            return location;
        }        

        /// <summary>
        /// Build list of Task<HttpResponseMessage> to process in parallel
        /// </summary>
        /// <param name="responses"></param>
        /// <returns></returns>
        private List<Task<string>> GetContentTasks(HttpResponseMessage[] responses)
        {
            List<Task<string>> contentTasks = new List<Task<string>>();
            foreach (HttpResponseMessage response in responses)
            {
                contentTasks.Add(response.Content.ReadAsStringAsync());
            }
            return contentTasks;
        }

        /// <summary>
        /// Call Google API for each mission
        /// </summary>
        /// <param name="missions"></param>
        /// <param name="originAddress"></param>
        /// <returns></returns>
        private async Task<List<Task<HttpResponseMessage>>> GetResponseTasks(List<Mission> missions, string originAddress)
        {
            List<Task<HttpResponseMessage>> responseTasks = new List<Task<HttpResponseMessage>>();

            (string api_key, string api_url) = await GetGoogleMapAPISettings();

            string googleAPI = GetGoogleAPI(originAddress, api_key, api_url);

            HttpClient client = new HttpClient();
            responseTasks.Add(client.GetAsync(googleAPI));

            string missinAddress = string.Empty;
            foreach (Mission mission in missions)
            {
                missinAddress = $"{mission.Address}, {mission.Country.Name}";
                googleAPI = GetGoogleAPI(missinAddress, api_key, api_url);
                responseTasks.Add(client.GetAsync(googleAPI));
            }

            return responseTasks;           
        }

        private string GetGoogleAPI(string address, string api_key, string api_url)
        {
            return $"{api_url}?address={address}&key={api_key}";
        }

        private async Task<(string api_key, string api_url)> GetGoogleMapAPISettings()
        {
            List<AppSetting> appSettings = await _appSettingRepository.GetAppSettings();
            string api_key = appSettings.Where(a => a.Name == "API_KEY").FirstOrDefault()?.Value;
            string api_url = appSettings.Where(a => a.Name == "GoogleMapURL").FirstOrDefault()?.Value;
            return (api_key, api_url);
        }

        /// <summary>
        /// Build dictionary that hold agent(key) and list of countries he operate
        /// </summary>
        /// <param name="missions"></param>
        /// <returns></returns>
        private Dictionary<int, List<string>> BuildAgentCountryDic(List<Mission> missions)
        {
            Dictionary<int, List<string>> agentCountries = new Dictionary<int, List<string>>();
            foreach (Mission mission in missions)
            {
                int agentId = mission.Agent.Id;
                if (!agentCountries.ContainsKey(agentId))
                {
                    agentCountries.Add(agentId, new List<string>());
                }
                agentCountries[agentId].Add(mission.Country.Name);
            }

            return agentCountries;
        }
        #endregion


    }
}
