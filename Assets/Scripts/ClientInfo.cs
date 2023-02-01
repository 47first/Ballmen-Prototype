using System.Text;
using UnityEngine;

namespace Ballmen.Client
{
    public interface IClientInfo 
    {
        public string Nickname { get; }
        public string Guid { get; }
        public byte[] GetBytes();
    }

    public struct ClientInfo : IClientInfo
    {
        public string nickname;
        public string guid;

        public ClientInfo(string nickname, string guid)
        {
            this.nickname = nickname;
            this.guid = guid;
        }

        public string Nickname => nickname;

        public string Guid => guid;

        public byte[] GetBytes()
        {
            var data = JsonUtility.ToJson(this);
            return Encoding.UTF8.GetBytes(data);
        }

        public static IClientInfo GetFromBytes(byte[] data)
        {
            Debug.Log($"There are {data.Length} bytes in data");
            string convertedData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<ClientInfo>(convertedData);
        }
    }

    public static class LocalClientInfo 
    {
        private static ClientInfo? _localClientInfo = null;

        [System.Obsolete] //Later will implement with Json serialization/deserialization
        public static ClientInfo GetLocal()
        {
            if (_localClientInfo == null)
            {
                Debug.Log("Initialize local client info");
                //Getting data from json
                _localClientInfo = new($"Player {UnityEngine.Random.Range(1, 10)}", System.Guid.NewGuid().ToString());
            }

            return _localClientInfo.Value;
        }
    }
}
