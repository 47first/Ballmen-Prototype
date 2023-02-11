namespace Ballmen.Session
{
    public enum GameTeam 
    {
        None,
        Red,
        Blue
    }

    internal static class TeamDistributor
    {
        internal static void DistributePlayersTeams(IPlayerStateContainer states) 
        {
            int blueTeamCount, redTeamCount;
            blueTeamCount = redTeamCount = 0;

            foreach(var guid in states.GetGuids())
            {
                var state = states.GetStateByGuid(guid);

                if (state.Team == GameTeam.None)
                {
                    GameTeam smallerTeam = redTeamCount > blueTeamCount ? GameTeam.Blue : GameTeam.Red;
                    state.ChangeTeam(smallerTeam);
                }

                switch (state.Team) 
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
