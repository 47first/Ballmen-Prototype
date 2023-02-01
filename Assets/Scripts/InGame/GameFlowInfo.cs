using Unity.Netcode;

namespace Ballmen.InGame
{
    internal interface IGameFlowInfo
    {
        internal NetworkVariable<int> RedTeamScore { get; }
        internal NetworkVariable<int> BlueTeamScore { get; }
    }

    public class GameFlowInfo : NetworkBehaviour, IGameFlowInfo
    {
        private static GameFlowInfo _instance;
        private NetworkVariable<int> _redTeamScore;
        private NetworkVariable<int> _blueTeamScore;

        internal static GameFlowInfo Singleton => _instance;
        NetworkVariable<int> IGameFlowInfo.RedTeamScore => _redTeamScore;
        NetworkVariable<int> IGameFlowInfo.BlueTeamScore => _blueTeamScore;

        private void Awake()
        {
            _redTeamScore = new(writePerm: NetworkVariableWritePermission.Server, readPerm: NetworkVariableReadPermission.Everyone);
            _blueTeamScore = new(writePerm: NetworkVariableWritePermission.Server, readPerm: NetworkVariableReadPermission.Everyone);
        }

        private void Start()
        {
            _instance = this;
        }
    }
}
