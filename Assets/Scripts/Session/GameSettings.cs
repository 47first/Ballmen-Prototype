namespace Ballmen.Session
{
    internal interface IGameSettings 
    {
        public int PlayerLimit { get; set; }
    }

    internal class GameSettings : IGameSettings
    {
        internal GameSettings(int playerLimit) 
        {
            PlayerLimit = playerLimit;
        }

        public static GameSettings Default => new GameSettings(2);

        public int PlayerLimit { get; set; }
    }
}
