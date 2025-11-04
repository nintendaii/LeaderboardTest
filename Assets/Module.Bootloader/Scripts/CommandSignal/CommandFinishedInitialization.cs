using System.Threading.Tasks;
using Module.Bootloader.Scripts.Controllers;
using Module.Common.Scripts;
using Module.Core.CommandSignal;
using UnityEngine.SceneManagement;
using Zenject;

namespace Module.Bootloader.Scripts.CommandSignal
{
    public class CommandFinishedInitialization: ICommand
    {
        [Inject] private readonly LoadingScreenController _loadingScreenController;
        public async void Execute()
        {
            await _loadingScreenController.FadeOut();
            var asyncOp = SceneManager.LoadSceneAsync(GlobalConstants.Scenes.APP_SCENE_NAME, LoadSceneMode.Single);
            if (asyncOp != null)
            {
                asyncOp.allowSceneActivation = true;

                while (!asyncOp.isDone)
                    await Task.Yield();
            }
        }
    }
}