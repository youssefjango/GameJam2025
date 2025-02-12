using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOffBlock : MonoBehaviour
{
    bool moveAway = false;
    bool once = false;
    private Collider2D collider2D;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ForeGround")&& !once)
        {
            moveAway = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ForeGround"))
        {
            once = true;
            moveAway = false;
        }
    }
    private void Start()
    {
        once = false;
        collider2D = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        collider2D.enabled = true;
        if (moveAway)
        {
            collider2D.enabled = false;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 4*Time.deltaTime);
        }
    }
}
