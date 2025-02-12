using UnityEngine;
using UnityEngine.Rendering.Universal; // For 2D Lights in URP

public class FlickerLight2D : MonoBehaviour
{
    [Header("Flicker Settings")]
    public float minIntensity = 0.5f; // Minimum light intensity
    public float maxIntensity = 1.5f; // Maximum light intensity
    public float flickerSpeed = 2f;   // Speed of the flickering effect

    private Light2D light2D;          // Reference to the 2D light component
    private float randomOffset;      // Offset for Perlin noise

    void Start()
    {
        // Get the Light2D component
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            Debug.LogError("No Light2D component found on this GameObject!");
        }

        // Generate a random offset for the flickering
        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (light2D != null)
        {
            // Calculate a flicker value using Perlin noise
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, randomOffset);
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

            // Apply the intensity to the light
            light2D.intensity = intensity;
        }
    }
}
