using System.Threading.Tasks;
using Module.Core.Scripts.MVC;
using SimplePopupManager;

namespace Module.App.Scripts.Controllers.Leaderboard
{
    public class LeaderboardMVC : MVCComponent<LeaderboardModel, LeaderboardView, LeaderboardController>,
        IPopupInitialization
    {
        
        async Task IPopupInitialization.Init(object param)
        {
            //TODO handle load
            //await Controller.LoadAndShowAsync();
        }
    }
}