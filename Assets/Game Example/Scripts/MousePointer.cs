using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public float glideSpeed = 5f; // Adjust the speed of gliding

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert screen coordinates to world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

        // Set the object's Z position to 0 to match the 2D plane
        mousePosition.z = 0;

        // Smoothly move the object toward the mouse position
        transform.position = Vector3.Lerp(transform.position, mousePosition, glideSpeed * Time.deltaTime);
    }
}
