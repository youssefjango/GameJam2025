using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    public float hoverScale = 1.2f;
    public float animationSpeed = 0.2f;
    public Color clickColor = Color.gray;
    private Color originalColor;
    private Image buttonImage;

    void Start()
    {
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale * hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            StartCoroutine(FlashClickEffect());
        }
    }

    IEnumerator ScaleButton(Vector3 targetScale)
    {
        float time = 0;
        Vector3 startScale = transform.localScale;
        while (time < animationSpeed)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / animationSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }

    IEnumerator FlashClickEffect()
    {
        buttonImage.color = clickColor;
        yield return new WaitForSeconds(0.1f);
        buttonImage.color = originalColor;
    }
}
