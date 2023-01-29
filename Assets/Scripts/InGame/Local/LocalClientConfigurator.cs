using Ballmen.Player;
using UnityEngine;

namespace Ballmen.InGame
{
    public class LocalClientConfigurator : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;

        internal void Configure() 
        {
            var localPlayerDecorator = PlayerDecorator.Local;

            Debug.Log($"localPlayerDecorator == null = {localPlayerDecorator == null}");

            _cameraController.SetTarget(localPlayerDecorator.transform);
        }
    }
}
