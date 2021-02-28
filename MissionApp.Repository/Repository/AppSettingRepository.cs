using Microsoft.EntityFrameworkCore;
using MissionApp.Data;
using MissionApp.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionApp.Repository
{
    public class AppSettingRepository : IAppSettingRepository
    {
        public MissionContext _missionContext;
        public AppSettingRepository(MissionContext missionContext)
        {
            _missionContext = missionContext;
        }
        public async Task<List<AppSetting>> GetAppSettings()
        {
            List<AppSetting> appSettings = null;
            try
            {
                appSettings = await _missionContext.AppSettings.ToListAsync();
            }
            catch (Exception ex)
            {
                //TODO:add logs and handle exception
            }

            return appSettings;

        }
    }
}
