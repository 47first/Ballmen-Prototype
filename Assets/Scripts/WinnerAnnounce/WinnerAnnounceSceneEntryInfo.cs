namespace Ballmen.WinnerAnnouncer
{
    internal sealed class WinnerAnnounceSceneEntryInfo
    {
        private static WinnerAnnounceSceneEntryInfo _instance;
        private GameResult _results;

        private WinnerAnnounceSceneEntryInfo() { }

        internal static WinnerAnnounceSceneEntryInfo Singleton => _instance;
        internal GameResult Results { get => _results; set => _results = value; }

        internal static void SetData(GameResult results) 
        {
            _instance ??= new WinnerAnnounceSceneEntryInfo();

            _instance.Results = results;
        }
    }
}
