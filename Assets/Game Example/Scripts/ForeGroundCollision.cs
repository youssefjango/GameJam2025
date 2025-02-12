using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeGroundCollision : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the foreground's TilemapCollider2D
        if (collision.collider.CompareTag("ForeGround") || collision.collider.CompareTag("Tree"))  // Make sure the TilemapCollider2D has the "Foreground" tag
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the tree (the GameObject this script is attached to)
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ForeGround") || collision.CompareTag("Tree"))  // Make sure the TilemapCollider2D has the "Foreground" tag
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the tree (the GameObject this script is attached to)
        }
    }

}
