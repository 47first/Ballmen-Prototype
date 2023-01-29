using Ballmen.Player;
using Ballmen.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ballmen.Server
{
    internal interface IPlayerDecoratorPull : System.IDisposable
    {
        internal void AddDecorator(string guid, PlayerDecorator playerInstance);
        internal PlayerDecorator GetDecorator(PlayerInfo playerInfo);
        internal PlayerDecorator GetDecorator(ulong id);
    }

    internal sealed class PlayerDeconratorsPull : IPlayerDecoratorPull
    {
        private Dictionary<string, PlayerDecorator> _pull = new();

        public void Dispose()
        {
            foreach (var guidDecorator in _pull)
                GameObject.Destroy(guidDecorator.Value.gameObject);

            _pull.Clear();
        }

        void IPlayerDecoratorPull.AddDecorator(string guid, PlayerDecorator playerInstance)
        {
            _pull.Add(guid, playerInstance);
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        PlayerDecorator IPlayerDecoratorPull.GetDecorator(PlayerInfo playerInfo)
        {
            if (_pull.TryGetValue(playerInfo.GUID.ToString(), out PlayerDecorator decorator))
                return decorator;

            throw new System.Exception("There isn't require player in pull");
        }

        PlayerDecorator IPlayerDecoratorPull.GetDecorator(ulong id)
        {
            return _pull.Values.First(decorator => decorator.OwnerClientId == id);
        }
    }
}
