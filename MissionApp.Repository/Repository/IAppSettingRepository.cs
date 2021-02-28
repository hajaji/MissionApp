using System.Collections.Generic;
using System.Threading.Tasks;
using MissionApp.Domain;

namespace MissionApp.Repository
{
    public interface IAppSettingRepository
    {
        Task<List<AppSetting>> GetAppSettings();
    }
}
