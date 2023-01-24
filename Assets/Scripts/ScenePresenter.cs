using UnityEngine;

namespace Ballmen.Scene
{
    [DisallowMultipleComponent]
    public abstract class ScenePresenter : MonoBehaviour
    {
        protected abstract void OnEnteringScene();
        protected void Start() => OnEnteringScene();
    }
}