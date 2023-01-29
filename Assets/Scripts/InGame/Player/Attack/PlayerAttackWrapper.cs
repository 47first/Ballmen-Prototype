using UnityEngine;

namespace Ballmen.Player
{
    internal interface IPlayerAttack 
    {
        internal void Kick();
        internal void KickFlip();
    }

    internal class PlayerAttackWrapper : IPlayerWrapper
    {
        private IPlayerAttack _playerAttack;
        internal PlayerAttackWrapper(IPlayerAttack playerAttack)
        {
            _playerAttack = playerAttack;
        }

        internal void HandleAttackCommand() 
        {
            if (Input.GetKeyDown(KeyCode.X))
                _playerAttack.Kick();

            if (Input.GetKeyDown(KeyCode.Z))
                _playerAttack.KickFlip();
        }
    }
}
