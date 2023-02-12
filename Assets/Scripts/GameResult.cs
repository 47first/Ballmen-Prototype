using Ballmen.Session;
using Unity.Netcode;

namespace Ballmen.WinnerAnnouncer
{
    internal struct GameResult : INetworkSerializable
    {
        internal GameTeam _winnerTeam;

        internal GameResult(GameTeam winnerTeam) 
        {
            _winnerTeam = winnerTeam;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            if (serializer.IsReader)
            {
                var reader = serializer.GetFastBufferReader();
                reader.ReadValueSafe(out _winnerTeam);
            }

            else
            {
                var writer = serializer.GetFastBufferWriter();
                writer.WriteValueSafe(_winnerTeam);
            }
        }
    }
}
