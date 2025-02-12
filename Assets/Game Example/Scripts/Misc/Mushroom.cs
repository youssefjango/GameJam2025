using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Mushroom : MonoBehaviour
{
    [Header("Settings")]
    public float detectionRadius = 3f;  // Player detection range
    public float lightFadeSpeed = 2f;   // How fast the light fades

    [Header("References")]
    public Animator animator;
    public Light2D light2D;
    public AudioSource growSound;
    public AudioSource shrinkSound;

    private GameObject player;
    private bool isGrowing = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        light2D.intensity = 0f;  // Ensure light starts off
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        bool playerIsClose = distance <= detectionRadius;
        bool playerIsFar = distance <= detectionRadius+6f;
        if (playerIsClose && !isGrowing)
        {
            StartGrowing();
        }
        else if (!playerIsClose && isGrowing)
        {
            StartShrinking();
        }
    }

    void StartGrowing()
    {
        isGrowing = true;
        animator.SetTrigger("Grow");
        if (growSound) growSound.Play();

        StopAllCoroutines();  // Stop previous fade
        StartCoroutine(FadeLight(0.4f));  // Fade light in
    }

    void StartShrinking()
    {
        isGrowing = false;
        animator.SetTrigger("Shrink");
        if (shrinkSound) shrinkSound.Play();

        StopAllCoroutines();  // Stop previous fade
        StartCoroutine(FadeLight(0f));  // Fade light out
    }

    IEnumerator FadeLight(float targetIntensity)
    {
        if (light2D == null) yield break;

        while (Mathf.Abs(light2D.intensity - targetIntensity) > 0.01f)
        {
            light2D.intensity = Mathf.Lerp(light2D.intensity, targetIntensity, Time.deltaTime * lightFadeSpeed);
            yield return null;
        }

        light2D.intensity = targetIntensity; // Ensure exact final value
    }
}
