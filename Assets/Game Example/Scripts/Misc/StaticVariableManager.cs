using System.Collections.Generic;
using UnityEngine;

public class StaticVariableManager : MonoBehaviour
{
    public static bool intro = true;
    public static int score; // Example static variable
    public static int money;
    public static bool isDead; // Example static variable
    public static float timer; // Example static variable
    public static float upgradeHp = 1;
    public static float upgradeDmg = 1;
    public static float upgradeMp = 1;
    private static StaticVariableManager _instance;

    // Singleton pattern to ensure only one instance persists
    void Awake()
    {
        if (upgradeHp == null)
        {
            upgradeHp = 1;
            upgradeDmg = 1;
            upgradeMp = 1;
        }
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed when loading a new scene
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

}
