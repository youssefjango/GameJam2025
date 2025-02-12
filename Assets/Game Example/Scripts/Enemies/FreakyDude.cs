using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreakyDude : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position,transform.position) > 7 || Vector2.Distance(player.transform.position, transform.position) < 4.5)
        {
            Destroy(gameObject);
        }
    }
}
