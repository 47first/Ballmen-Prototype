using System.Collections.Generic;

namespace Ballmen.Scene
{
    internal enum SceneEnum
    {
        MainMenu, 
        Lobby,
        InGame,
        WinnerAnnounce
    }

    internal class SceneNames
    {
        private static readonly Dictionary<SceneEnum, string> _names;
        static SceneNames()
        {
            _names = new()
            {
                { SceneEnum.MainMenu, "MainMenu" },
                { SceneEnum.InGame, "InGame" },
                { SceneEnum.Lobby, "Lobby" },
                { SceneEnum.WinnerAnnounce, "WinnerAnnounce" }
            };
        }

        internal static string GetByEnum(SceneEnum sceneEnum) 
        {
            return _names[sceneEnum];
        }
    }
}
