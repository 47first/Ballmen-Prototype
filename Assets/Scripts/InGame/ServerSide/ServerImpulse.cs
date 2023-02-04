using System.Collections.Generic;
using UnityEngine;

namespace Ballmen.InGame.Server
{
    internal sealed class ServerImpulse : Impulse
    {
        private LayerMask _impulseLayerMask;

        internal ServerImpulse(LayerMask layerMask)
        {
            _impulseLayerMask = layerMask;
        }

        internal override Dictionary<IPunchable, Vector3> GetPunchListByBox(Vector3 pos, Vector3 size, float force)
        {
            return _punchList;
        }

        internal override Dictionary<IPunchable, Vector3> GetPunchListByRadius(Vector3 pos, float radius, float force)
        {
            var colliders = Physics.OverlapSphere(pos, radius, _impulseLayerMask);

            TryAddColidersInList(pos, colliders, force);

            return _punchList;
        }

        private void TryAddColidersInList(Vector3 impulsePosition, Collider[] colliders, float force) 
        {
            _punchList.Clear();

            foreach (var collider in colliders)
                if(collider.TryGetComponent<IPunchable>(out IPunchable punchable))
                    _punchList.Add(punchable, (collider.transform.position -
                        impulsePosition).normalized * force);
        }
    }
}
