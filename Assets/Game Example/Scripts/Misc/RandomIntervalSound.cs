using System.Collections;
using UnityEngine;

public class RandomAction : MonoBehaviour
{
    public float minInterval = 10f;  // Minimum interval in seconds
    public float maxInterval = 20f;  // Maximum interval in seconds
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        StartCoroutine(PerformActionAtRandomIntervals());
    }

    IEnumerator PerformActionAtRandomIntervals()
    {
        while (true)
        {
            // Wait for a random interval between minInterval and maxInterval
            float randomInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(randomInterval);

            // Perform the action here (e.g., print a message)
            PerformRandomAction();
        }
    }

    void PerformRandomAction()
    {
        switch(Random.Range(1, 3)) {
            case 1:
                audioManager.PlaySFX(audioManager.caveSound1);
                break;
            case 2:
                audioManager.PlaySFX(audioManager.caveSound2);
                break;
            case 3:
                audioManager.PlaySFX(audioManager.caveSound3);
                break;
        }
        
    }
}
