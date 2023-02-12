using Ballmen.Scene;
using UnityEngine;

namespace Ballmen.WinnerAnnouncer
{
    internal sealed class WinnerAnnounceScenePresenter : ScenePresenter
    {
        [SerializeField] private WinnerAnnounceView _view;

        protected override void OnEnteringScene()
        {
            var resultsData = WinnerAnnounceSceneEntryInfo.Singleton.Results;
            _view.UpdateView(resultsData);
        }
    }
}
