using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class NodeFieldCreation : MonoBehaviour
{

    [SerializeField] private GameObject prefab; // The prefab to instantiate
    public float gridSizeX = 3; // Number of prefabs to instantiate along the X-axis
    public float gridSizeY = 3; // Number of prefabs to instantiate along the Y-axis
    //[SerializeField] private float interval = 1f; // Interval between each instantiation

    void Awake()
    {
       
        StartCoroutine(InstantiatePrefabs());
        
    }

    // Coroutine to instantiate prefabs at a given interval
    IEnumerator InstantiatePrefabs()
    {
        float totalObjects = gridSizeX * gridSizeY;
        float currentCount = 0;
        int batchSize = 50; // Number of objects to instantiate per frame

        for (float x = 0; x < gridSizeX; x += 1)
        {
            for (float y = 0; y < gridSizeY; y += 1)
            {
                // Calculate the position for each prefab
                Vector3 position = new Vector3(x, y, 0); // Adjust the Z if needed

                GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
                newObject.transform.SetParent(transform, false);

                currentCount++;
                

                // Wait after every batch
                if (currentCount % batchSize == 0)
                {
                    yield return null;
                }
            }
        }

    }

}