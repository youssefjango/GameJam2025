using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public GameObject Score;
    public UnityEngine.UI.Text scoreText; // Assign your Text UI element in the Inspector
    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{StaticVariableManager.money}";
    }
}
