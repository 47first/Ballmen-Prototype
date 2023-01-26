using Ballmen.Player;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Ballmen.InGame
{
    internal class ImpulseManager : NetworkBehaviour
    {
        [SerializeField] private LayerMask _impulseLayerMask;
        [SerializeField] private List<PlayerDecorator> _players;

        internal void KickPlayersInRadius(Vector3 impulsePoint, float radius, float force)
        {
            var colliders = Physics.OverlapSphere(impulsePoint, radius, _impulseLayerMask);

            foreach (var collider in colliders)
            {
                var player = _players.Find(decorator => decorator.gameObject == collider.gameObject);

                player.GetKickedClientRpc((impulsePoint - player.transform.position).normalized, force);
            }
        }

        private void OnDrawGizmos()
        {
            
        }
    }
}
