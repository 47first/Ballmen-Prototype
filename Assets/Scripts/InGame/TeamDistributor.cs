using Ballmen.Session;

namespace Ballmen.Server
{
    internal sealed class TeamDistributor
    {
        private IPlayerDecoratorsPull _playerDecoratorsPull;
        internal TeamDistributor(IPlayerDecoratorsPull playerDecoratorsPull) 
        {
            _playerDecoratorsPull = playerDecoratorsPull;
        }

        internal void DistributePlayers() 
        {
            int blueTeamCount, redTeamCount;
            blueTeamCount = redTeamCount = 0;
        }
    }
}
