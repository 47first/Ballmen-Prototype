using System;
using Unity.Collections;
using Unity.Netcode;
using Ballmen.Client;

namespace Ballmen.Session
{
    public struct PlayerInfo : INetworkSerializable, IEquatable<PlayerInfo>
    {
        private ulong _id;
        private FixedString128Bytes _nickname;

        public PlayerInfo(ulong id, FixedString128Bytes nickname)
        {
            _id = id;
            _nickname = nickname;
        }

        public PlayerInfo(ulong id, IClientInfo clientInfo) 
        {
            _id = id;
            _nickname = clientInfo.Nickname;
        }

        public ulong Id => _id; 

        public FixedString128Bytes Nickname => _nickname;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            if (serializer.IsReader)
            {
                var reader = serializer.GetFastBufferReader();
                reader.ReadValueSafe(out _id);
                reader.ReadValueSafe(out _nickname);
            }

            else
            {
                var writer = serializer.GetFastBufferWriter();
                writer.WriteValueSafe(_id);
                writer.WriteValueSafe(_nickname);
            }
        }

        public bool Equals(PlayerInfo other)
        {
            return Id == other.Id && Nickname == other.Nickname;
        }
    }
}
