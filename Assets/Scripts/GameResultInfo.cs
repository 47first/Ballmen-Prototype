using Ballmen.Client;
using Ballmen.Session;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace Ballmen.GameResults
{
    internal interface IGameResult 
    {
        
    }

    internal struct GameResult : IGameResult
    {


        public byte[] GetBytes()
        {
            var data = JsonUtility.ToJson(this);
            return Encoding.UTF8.GetBytes(data);
        }

        public static IGameResult GetFromBytes(byte[] data)
        {
            Debug.Log($"There are {data.Length} bytes in game result");
            string convertedData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<GameResult>(convertedData);
        }
    }
}
