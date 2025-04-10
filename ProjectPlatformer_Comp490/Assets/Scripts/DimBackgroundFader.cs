using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimBackgroundFader : MonoBehaviour
{
    [Tooltip("CanvasGroup on the background panel.")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Tooltip("The target alpha value when the background is fully faded in.")]
    [SerializeField] private float targetAlpha = 0.5f;

    [Tooltip("The duration of the fade-in in seconds.")]
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup());
    }

    private IEnumerator FadeCanvasGroup()
    {
        float elapsed = 0f;
        // Fade in
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
    }
}
