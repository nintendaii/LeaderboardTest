using UnityEngine;

namespace Module.Common.Scripts
{
    public static class GlobalConstants
    {
        public static class Resources
        {
            public static readonly string LEADERBOARD_JSON_PATH = "Leaderboard";
            public static readonly string AVATAR_CACHE_FOLDER_PATH = "AvatarCache";
        }
        
        public static class Addressable
        {
            public static readonly string LEADERBOARD_ADDRESSABLE_PATH = "Assets/Module.App/LeaderboardComponent.prefab";
        }
        
        public static class Record
        {
            public static readonly Color RANK_DEFAULT_COLOR = Color.black;
            public static readonly Color RANK_BRONZE_COLOR = new Color(0.8f, 0.5f, 0.3f);
            public static readonly Color RANK_SILVER_COLOR = new Color(0.75f, 0.75f, 0.75f);
            public static readonly Color RANK_GOLD_COLOR = new Color(1f, 0.84f, 0f);
            public static readonly Color RANK_DIAMON_COLOR = new Color(0.18f, 0.8f, 0.94f);

            public static readonly float RANK_DEFAULT_SIZE = 1f;
            public static readonly float RANK_BRONZE_SIZE = 1.2f;
            public static readonly float RANK_SILVER_SIZW = 1.4f;
            public static readonly float RANK_GOLDEN_SIZE = 1.6f;
            public static readonly float RANK_DIAMOND_SIZE = 1.8f;
        }
        
        public static class Scenes
        {
            public static readonly string APP_SCENE_NAME = "AppScene";

        }
    }
}