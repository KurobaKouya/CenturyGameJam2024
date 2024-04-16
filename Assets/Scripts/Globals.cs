public class Globals
{
    public static int playerHealth = 100;
    public static int playerDmg = 10;
    public static float playerMovementSpeed = 10f;
    public static float sprintMovementSpeed = 20f;
    public static float healthRegen = 5f;


    public  static float attackStaminaDrain = 15f;
    public static float sprintStaminaDrain = 5f;
    public static float staminaRegen = 5f;


    public static float flashlightRegen = 10f;
    public static float flashlightDrain = 5.0f;
    public static float flashlightDrainOnHit = 20f;

    public static float maxInk = 100;
    public static float inkPerDistance = 0.5f;


    public static int monsterHealth = 20;
    public static int monsterDmg = 20;
    public static float monsterSightRange = 10f;
    public static float monsterPatrolRange = 10f;
    public static float maxDistanceFromPlayer = 30f;
    public static float minSpawnDistance = 20f;
    public static float spawnVariance = 20f;
    public static int maxSpawnCount = 15;


    public enum SceneIndex
    {
        Preload         = 0,
        MainMenu        = 1,
        Game            = 2,
    }


    public enum ItemIndex
    {
        None = -1,
        Axe = 0,
        Relic = 1,
    }
}
