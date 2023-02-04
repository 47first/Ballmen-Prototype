using System.Collections.Generic;
using UnityEngine;

namespace Ballmen.InGame
{
    internal interface IPunchable
    {
        internal void GetPunched(Vector3 direction);
    }
}

namespace Ballmen.InGame.Server
{
    internal abstract class Impulse
    {
        protected Dictionary<IPunchable, Vector3> _punchList = new();
        internal abstract Dictionary<IPunchable, Vector3> GetPunchListByRadius(Vector3 pos, float radius, float force);
        internal abstract Dictionary<IPunchable, Vector3> GetPunchListByBox(Vector3 pos, Vector3 size, float force);
    }
}
