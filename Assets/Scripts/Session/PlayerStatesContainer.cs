using System.Collections.Generic;
using System.Linq;

namespace Ballmen.Session 
{
    internal interface IPlayerStateContainer
    {
        internal void AddState(string guid);
        internal void RemoveState(string guid);
        internal bool Contains(string guid);
        internal string[] GetGuids();
        internal PlayerState GetStateByGuid(string guid);
    }

    internal sealed class PlayerStateContainer : IPlayerStateContainer
    {
        private Dictionary<string, PlayerState> _playerStates = new();

        void IPlayerStateContainer.AddState(string guid) => _playerStates.TryAdd(guid, new());
        
        void IPlayerStateContainer.RemoveState(string guid) => _playerStates.Remove(guid);

        string[] IPlayerStateContainer.GetGuids() => _playerStates.Keys.ToArray();

        PlayerState IPlayerStateContainer.GetStateByGuid(string guid) => _playerStates[guid];

        bool IPlayerStateContainer.Contains(string guid) => _playerStates.ContainsKey(guid);
    }

    internal class PlayerState
    {
        private GameTeam _team;
        internal PlayerState() 
        {
            _team = GameTeam.None;
        }

        public GameTeam Team => _team;

        public void ChangeTeam(GameTeam team) => _team = team;
    }
}
