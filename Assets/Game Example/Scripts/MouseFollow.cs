using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private float rotationOffset = 0f; // Optional rotation offset
    [SerializeField] private GameObject MousePointer;  // GameObject to point towards

    private void Update()
    {
        FaceMousePointer();
    }

    private void FaceMousePointer()
    {
        // Get the MousePointer's position (already in world space)
        Vector3 mousePosition = MousePointer.transform.position;

        // Calculate the direction from this object to the MousePointer
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle in degrees from the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation offset
        angle += rotationOffset;

        // Rotate the object to face the MousePointer
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
