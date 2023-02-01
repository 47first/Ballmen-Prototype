using Ballmen.Player;
using Ballmen.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ballmen.Server
{
    internal interface IPlayerDecoratorsPull : System.IDisposable
    {
        internal void AddDecorator(string guid, PlayerDecorator playerInstance);
        internal PlayerDecorator GetDecorator(PlayerInfo playerInfo);
        internal PlayerDecorator GetDecorator(ulong id);
    }

    // Using to respawn associated decorator with player that reconnects to game
    internal sealed class PlayerDecoratorsPull : IPlayerDecoratorsPull
    {
        private Dictionary<string, PlayerDecorator> _pull = new();

        void IPlayerDecoratorsPull.AddDecorator(string guid, PlayerDecorator playerInstance)
        {
            Debug.Log($"Add {guid}");

            _pull.Add(guid, playerInstance);
        }

        void IDisposable.Dispose()
        {
            foreach (var guidDecorator in _pull)
                GameObject.Destroy(guidDecorator.Value.gameObject);

            _pull.Clear();
        }

        PlayerDecorator IPlayerDecoratorsPull.GetDecorator(PlayerInfo playerInfo)
        {
            Debug.Log($"Try get {playerInfo.GUID.ToString()}");

            if (_pull.TryGetValue(playerInfo.GUID.ToString(), out PlayerDecorator decorator))
                return decorator;

            throw new System.Exception("There isn't require player in pull");
        }

        PlayerDecorator IPlayerDecoratorsPull.GetDecorator(ulong id)
        {
            return _pull.Values.First(decorator => decorator.OwnerClientId == id);
        }
    }
}