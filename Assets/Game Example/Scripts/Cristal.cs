using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class Cristal: MonoBehaviour
{
    public GameObject mainCamera;
    private int maxObjects;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.Collect);
            collect();

        }
    }

    void collect() {
        StaticVariableManager.score ++;
        StartCoroutine(MoveToTopLeft());
        if (StaticVariableManager.score == maxObjects) { 
        //end game probably? event?
        }
    }
    IEnumerator MoveToTopLeft()
    {
        GetComponent<Collider2D>().enabled = false;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = mainCamera.transform.position + new Vector3(-7f, 4f, 0);
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = endPosition;
        Destroy(gameObject);
    }

}
