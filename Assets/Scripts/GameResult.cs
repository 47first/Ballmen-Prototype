using Ballmen.Session;
using System.Text;
using UnityEngine;

namespace Ballmen.GameResults
{
    internal struct GameResult
    {
        internal GameTeam _winnerTeam;

        internal GameResult(GameTeam winnerTeam) 
        {
            _winnerTeam = winnerTeam;
        }

        public byte[] GetBytes()
        {
            var data = JsonUtility.ToJson(this);
            return Encoding.UTF8.GetBytes(data);
        }

        public static GameResult GetFromBytes(byte[] data)
        {
            Debug.Log($"There are {data.Length} bytes in game result");
            string convertedData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<GameResult>(convertedData);
        }
    }
}
