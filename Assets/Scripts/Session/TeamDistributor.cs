using System.Collections.Generic;
using System.Linq;

namespace Ballmen.Session
{
    internal sealed class TeamDistributor
    {
        private Dictionary<string, GameTeam> _playerTeam = new();

        internal GameTeam GetTeamByGuid(string guid) 
        {
            return _playerTeam[guid];
        }

        internal void AddPlayer(string guid) 
        {
            _playerTeam.TryAdd(guid, GameTeam.None);
        }

        internal void RemovePlayer(string guid) 
        {
            _playerTeam.Remove(guid);
        }

        internal void ChangeTeam(string guid, GameTeam team) 
        {
            _playerTeam[guid] = team;
        }

        internal void DistributePlayersTeams() 
        {
            int blueTeamCount, redTeamCount;
            blueTeamCount = redTeamCount = 0;

            foreach(var key in _playerTeam.Keys.ToList())
            {
                if (_playerTeam[key] == GameTeam.None)
                {
                    GameTeam smallerTeam = redTeamCount > blueTeamCount ? GameTeam.Blue : GameTeam.Red;
                    ChangeTeam(key, smallerTeam);
                }

                switch (_playerTeam[key]) 
                {
                    case GameTeam.Red:
                        redTeamCount++;
                        break;

                    case GameTeam.Blue:
                        blueTeamCount++;
                        break;
                }
            }
        }
    }
}
