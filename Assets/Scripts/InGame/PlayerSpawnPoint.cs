using Ballmen.Session;
using UnityEngine;

namespace Ballmen.InGame
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameTeam _team;
        [SerializeField] private LayerMask _obstaclesLayerMask;

        internal GameTeam Team => _team;
        internal Vector3 Position => transform.position;

        internal bool CanSpawn() 
        {
            return Physics.CheckBox(transform.position, Vector3.one, transform.rotation, _obstaclesLayerMask);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position, Vector3.one * 2);
        }
        #endif
    }
}
