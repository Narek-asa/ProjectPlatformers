using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteUIManager : MonoBehaviour
{
    [Tooltip("CanvasGroup component for fading in/out.")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Tooltip("Duration of the fade-in effect in seconds.")]
    [SerializeField] private float fadeDuration = 0.5f;

    [Tooltip("Initial scale for the animation.")]
    [SerializeField] private Vector3 startScale = new Vector3(0.8f, 0.8f, 0.8f);
    [Tooltip("Time to scale from startScale to full scale.")]
    [SerializeField] private float scaleDuration = 0.5f;

    private void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        transform.localScale = startScale;
    }

    public void ShowLevelCompleteScreen()
    {
        StartCoroutine(FadeAndScaleIn());
    }

    private IEnumerator FadeAndScaleIn()
    {
        float timer = 0f;

        // Fade in and scale
        while (timer < fadeDuration || timer < scaleDuration)
        {
            timer += Time.deltaTime;
            float tFade = Mathf.Clamp01(timer / fadeDuration);
            float tScale = Mathf.Clamp01(timer / scaleDuration);

            canvasGroup.alpha = Mathf.Lerp(0, 1, tFade);
            transform.localScale = Vector3.Lerp(startScale, Vector3.one, tScale);

            yield return null;
        }

        canvasGroup.alpha = 1;
        transform.localScale = Vector3.one;
    }
}
