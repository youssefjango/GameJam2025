using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawner : MonoBehaviour
{
    public GameObject enemyPrefab1; // Enemy prefab to spawn
    public GameObject enemyPrefab2; // Enemy prefab to spawn
    // Start is called before the first frame update
    void Start()
    {
        switch (UnityEngine.Random.Range(1,2))
        {
            case 1:
                enemyPrefab1.SetActive(true);
                enemyPrefab2.SetActive(false);
                break;
            case 2:
                enemyPrefab1.SetActive(false);
                enemyPrefab2.SetActive(true);
                break;
        }
        
    }

}
