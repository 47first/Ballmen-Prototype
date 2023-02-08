namespace Ballmen.Session
{
    internal interface IGameSettings 
    {
        public int PlayerLimit { get; set; }
        public int ScoreLimit { get; set; }
    }

    internal class GameSettings : IGameSettings
    {
        internal GameSettings(int playerLimit, int scoreLimit) 
        {
            PlayerLimit = playerLimit;
            ScoreLimit = scoreLimit;
        }

        public static GameSettings Default => new GameSettings(2, 3);

        public int PlayerLimit { get; set; }
        public int ScoreLimit { get; set; }
    }
}
