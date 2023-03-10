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
        private FixedString128Bytes _guid;

        public PlayerInfo(ulong id, FixedString128Bytes nickname, FixedString128Bytes guid)
        {
            _id = id;
            _nickname = nickname;
            _guid = guid;
        }

        internal PlayerInfo(ulong id, IClientInfo clientInfo) 
        {
            _id = id;
            _nickname = clientInfo.Nickname;
            _guid = clientInfo.Guid;
        }

        public ulong Id => _id;

        public FixedString128Bytes Nickname => _nickname;

        public FixedString128Bytes GUID => _guid;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            if (serializer.IsReader)
            {
                var reader = serializer.GetFastBufferReader();
                reader.ReadValueSafe(out _id);
                reader.ReadValueSafe(out _nickname);
                reader.ReadValueSafe(out _guid);
            }

            else
            {
                var writer = serializer.GetFastBufferWriter();
                writer.WriteValueSafe(_id);
                writer.WriteValueSafe(_nickname);
                writer.WriteValueSafe(_guid);
            }
        }

        public bool Equals(PlayerInfo other)
        {
            return _guid == other.GUID;
        }
    }
}
