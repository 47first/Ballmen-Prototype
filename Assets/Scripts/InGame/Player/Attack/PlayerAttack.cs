using Ballmen.InGame.Server;
using UnityEngine;

namespace Ballmen.InGame.Player
{
    internal class LocalPlayerAttack : IPlayerAttack
    {
        private PlayerDecorator _player;
        private const float _radius = 50;
        private const float _force = 50;

        internal LocalPlayerAttack(PlayerDecorator player)
        {
            _player = player;
        }

        void IPlayerAttack.Kick()
        {
            ServerImpulseCreator.Singleton.CreateImpulseInRadiusServerRpc(
                _player.transform.position, _radius, _force, default);
        }

        void IPlayerAttack.KickFlip()
        {
            ServerImpulseCreator.Singleton.CreateImpulseInRadiusServerRpc(
                _player.transform.position, _radius, _force, default);
        }
    }
}
