using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBuffer : MonoBehaviour
{
    public float detectionRadius = 7f;  // Player detection range
    private GameObject player;
    public Animator animator;
    public Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool playerIsClose = distance <= detectionRadius;
        if (playerIsClose)
        {
            if (collider != null)
                collider.enabled = true;
            animator.enabled = true;
        }
        else
        {
            if (collider != null)
                collider.enabled = false;
            animator.enabled = false;
        }
    }
}
