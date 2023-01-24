using System.Text;
using UnityEngine;

namespace Ballmen.Client
{
    public interface IClientInfo 
    {
        public string Nickname { get; }
        public byte[] GetBytes();
    }

    public struct ClientInfo : IClientInfo
    {
        public string nickname;

        private ClientInfo(string nickname)
        {
            this.nickname = nickname;
        }

        public string Nickname => nickname;

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

        [System.Obsolete] //Later will implement with Json serialization/deserialization
        public static ClientInfo GetLocal() => new($"Player {UnityEngine.Random.Range(1, 10)}");
    }
}