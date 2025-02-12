using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("selfDestruct", 8);
    }
    void selfDestruct()
    {
        Destroy(gameObject);
    }
}
