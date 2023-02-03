using Ballmen.Session;
using System.Collections.Generic;
using Unity.Netcode;

namespace Ballmen.Server
{
    internal sealed class TeamDistributor
    {
        internal static void DistributePlayersTeams(NetworkList<PlayerInfo> playerInfos) 
        {
            int blueTeamCount, redTeamCount;
            blueTeamCount = redTeamCount = 0;

            foreach (var playerInfo in playerInfos) 
            {
                if (playerInfo.Team == GameTeam.None)
                {
                    GameTeam smallerTeam = redTeamCount > blueTeamCount ? GameTeam.Blue : GameTeam.Red;
                    playerInfo.SetTeam(smallerTeam);
                }

                switch (playerInfo.Team) 
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
