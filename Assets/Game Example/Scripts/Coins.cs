using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        StartCoroutine(MoveToTopLeft());
    }

    // Update is called once per frame
    IEnumerator MoveToTopLeft()
    {
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
