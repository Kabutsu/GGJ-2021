using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class GameTag
    {
        public const string Player = "Player";
        public const string PlayerItem = "PlayerItem";
        public const string Enemy = "Enemy";
        public const string Respawn = "Respawn";
        public const string MainCamera = "MainCamera";
        public const string Environment = "Scenery";

        public static bool IsPlayer(GameObject gameObject)
            => gameObject.CompareTag(Player) || gameObject.CompareTag(PlayerItem);

        public static bool IsWorldItem(GameObject gameObject)
            => gameObject.CompareTag(Enemy) || gameObject.CompareTag(Environment);
    }
}
