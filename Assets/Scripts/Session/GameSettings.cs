namespace Ballmen.Session
{
    internal interface IGameSettings 
    {
        public int PlayerLimit { get; set; }
        public float InitialHealth { get; set; }
        public int MaxCardAmount { get; set; }
        public bool UseSpecialCards { get; set; }
        public bool RefillCards { get; set; }
    }

    internal class GameSettings : IGameSettings
    {
        public GameSettings(int playerLimit, float initialHealth, 
            int maxCardAmount, bool useSpecialCards, bool refillCards)
        {
            PlayerLimit = playerLimit;
            InitialHealth = initialHealth;
            MaxCardAmount = maxCardAmount;
            UseSpecialCards = useSpecialCards;
            RefillCards = refillCards;
        }

        public readonly static GameSettings Default = new(2, 100, 3, true, true);

        public int PlayerLimit { get; set; }

        public float InitialHealth { get; set; }

        public int MaxCardAmount { get; set; }

        public bool UseSpecialCards { get; set; }

        public bool RefillCards { get; set; }
    }
}
