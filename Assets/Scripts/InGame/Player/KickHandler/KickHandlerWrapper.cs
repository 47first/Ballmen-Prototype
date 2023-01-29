using UnityEngine;

namespace Ballmen.Player
{
    internal interface IKickHandler 
    {
        internal void HandleKick(Vector3 direction);
    }

    internal class KickHandlerWrapper : IPlayerWrapper
    {
        private IKickHandler _kickHandler;
        internal KickHandlerWrapper(IKickHandler kickHander) 
        {
            _kickHandler = kickHander;
        }
        
        internal void HandleKick(Vector3 direction) 
        {
            _kickHandler.HandleKick(direction);
        }
    }
}
