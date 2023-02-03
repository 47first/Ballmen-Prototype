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

            for(int i = 0; i < playerInfos.Count; i++) 
            {
                if (playerInfos[i].Team == GameTeam.None)
                {
                    GameTeam smallerTeam = redTeamCount > blueTeamCount ? GameTeam.Blue : GameTeam.Red;
                    playerInfos[i] = new();.Team = smallerTeam;
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
