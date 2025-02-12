using UnityEngine;

public class SubtleTitleAnimation : MonoBehaviour
{
    public SpriteRenderer titleSprite;   // Drag your SpriteRenderer here in the Inspector
    public float scaleSpeed = 0.2f;      // Speed of the scaling effect
    public float fadeSpeed = 0.5f;       // Speed of fading in and out
    public float shakeAmount = 0.03f;    // Subtle shaking amount
    public float shakeSpeed = 0.5f;      // Speed of the shake (lower is slower)

    private Vector3 originalPosition;    // The original position of the title
    private float alpha = 0f;            // The alpha value for fading in and out

    void Start()
    {
        originalPosition = titleSprite.transform.position;
        alpha = 0f;  // Start with the title invisible
    }

    void Update()
    {
        // Subtle shake effect
        ShakeTitle();

        // Slow pulsating scale effect
        PulsateScale();

        // Fade in effect for the title to appear gradually
        FadeIn();
    }

    void ShakeTitle()
    {
        // Add slight shake based on sine wave for smooth, subtle movement
        float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        float shakeY = Mathf.Cos(Time.time * shakeSpeed) * shakeAmount;
        titleSprite.transform.position = originalPosition + new Vector3(shakeX, shakeY, 0f);
    }

    void PulsateScale()
    {
        // Create a subtle pulsing effect for the title's scale
        float scale = Mathf.PingPong(Time.time * scaleSpeed, 0.1f) + 1.2f; // Pulsing between 1 and 1.1 scale
        titleSprite.transform.localScale = new Vector3(scale, scale, 1f);
    }

    void FadeIn()
    {
        // Gradually fade the title in
        alpha = Mathf.MoveTowards(alpha, 1f, fadeSpeed * Time.deltaTime);  // Fade towards fully visible
        titleSprite.color = new Color(titleSprite.color.r, titleSprite.color.g, titleSprite.color.b, alpha);
    }
}
