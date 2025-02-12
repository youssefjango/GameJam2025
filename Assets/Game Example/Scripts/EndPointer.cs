using UnityEngine;

public class PointAtTarget : MonoBehaviour
{
    public GameObject pointerSprite;

    public LoadingScreenManager LoadingScreenScript;
    public GameObject LoadingScreenObject;

    [Header("Target Settings")]
    public GameObject target;  // The object this should point at

    [Header("Rotation Settings")]
    public bool smoothRotation = true; // Enable smooth rotation
    public float rotationSpeed = 5f; // Rotation speed (only applies if smoothRotation is true)

    void Start()
    {
        LoadingScreenObject = GameObject.Find("LoadScreenManager");
        LoadingScreenScript = LoadingScreenObject.GetComponent<LoadingScreenManager>();
    }

    void Update()
    {
        if (StaticVariableManager.score > 14)
        {
            pointerSprite.SetActive(true);
        }
        else
        {
            pointerSprite.SetActive(false);
        }
        if (LoadingScreenScript.gameStart)
        {
            target = GameObject.Find("Magic Ring(Clone)");
        }
        else if (target == null) return;

        // Get the direction to the target
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation (smooth or instant)
        if (smoothRotation)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
