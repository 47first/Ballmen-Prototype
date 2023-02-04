using System.Collections.Generic;
using System.Linq;

namespace Ballmen.Session 
{
    internal interface IPlayerStateContainer
    {
        internal void AddState(string guid);
        internal string[] GetGuids();
        internal PlayerState GetStateByGuid(string guid);
    }

    internal sealed class PlayerStateContainer : IPlayerStateContainer
    {
        private Dictionary<string, PlayerState> _playerStates = new();

        void IPlayerStateContainer.AddState(string guid) => _playerStates.TryAdd(guid, new());

        string[] IPlayerStateContainer.GetGuids() => _playerStates.Keys.ToArray();

        PlayerState IPlayerStateContainer.GetStateByGuid(string guid) => _playerStates[guid];

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
