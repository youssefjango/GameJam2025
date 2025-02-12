using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Size : MonoBehaviour
{
    
    public UnityEngine.UI.Slider LEVELSIZE;

    public float Val;
    public Transform bruh;
    public void Start()
    {
       bruh = GetComponent<Transform>();    
    }

    public void LEVEL_SIZE()
    {
        Val = (LEVELSIZE.value-30);
        PlayerPrefs.SetInt("SIZE", (int)LEVELSIZE.value);
        PlayerPrefs.Save();
        Vector2 squar = new Vector2(Val, Val);
        bruh.localScale = squar;  
        Debug.Log(Val);
        Debug.Log(LEVELSIZE.value);

    }
}


