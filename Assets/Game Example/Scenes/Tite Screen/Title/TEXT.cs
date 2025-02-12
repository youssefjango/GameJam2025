using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TEXT : MonoBehaviour
{

    public TextMeshProUGUI huh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       var textUI = huh.GetComponent<TextMeshProUGUI>();
        int SIZE = PlayerPrefs.GetInt("SIZE", 40);
      textUI.text = SIZE.ToString();
    }
}
