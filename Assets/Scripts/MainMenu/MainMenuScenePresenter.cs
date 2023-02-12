using Ballmen.Services;

namespace Ballmen.Scene
{
    public class MainMenuScenePresenter : ScenePresenter
    {
        protected override void OnEnteringScene()
        {
            var dependencyInjectionService = DependencyInjectionService.Singleton;
            dependencyInjectionService.Register<ISceneChanger>(new SceneChanger(), true);
        }
    }
}
