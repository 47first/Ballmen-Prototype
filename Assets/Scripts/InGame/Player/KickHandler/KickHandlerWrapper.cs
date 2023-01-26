using UnityEngine;

namespace Ballmen.Player
{
    internal interface IKickHandler 
    {
        internal void HandleKick(Vector3 direction, float force);
    }

    internal class KickHandlerWrapper
    {
        private IKickHandler _kickHandler;
        internal KickHandlerWrapper(IKickHandler kickHander) 
        {
            _kickHandler = kickHander;
        }
        
        internal void HandleKick(Vector3 direction, float force) 
        {
            _kickHandler.HandleKick(direction, force);
        }
    }
}
