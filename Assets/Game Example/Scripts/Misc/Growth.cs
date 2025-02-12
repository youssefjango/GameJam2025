using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    private float grow = 1f; // Initial scale factor
    public float growthRate = 0.5f; // How fast the object grows per second
    private bool isGrowing = false; // Flag to track collision state

    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLight"))
        {
            isGrowing = true; // Start growing
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLight"))
        {
            isGrowing = false; // Stop growing
        }
    }

    private void Update()
    {
        if (isGrowing)
        {
            // Increment the growth factor over time
            grow += growthRate * Time.deltaTime;

            // Update the object's scale
            transform.localScale = new Vector3(grow, grow, 1f); // Ensure the Z scale is 1 for 2D
        }
    }
}
