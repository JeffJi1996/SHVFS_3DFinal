using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeInOut : MonoBehaviour
{
    private float UI_Alpha = 0;
    public float alphaSpeed;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (canvasGroup == null)
        {
            return;
        }

        if (UI_Alpha != canvasGroup.alpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, UI_Alpha, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(UI_Alpha - canvasGroup.alpha) <= 0.01f)
            {
                canvasGroup.alpha = UI_Alpha;
            }
        }
    }

    public void FadeIn()
    {
        UI_Alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void FadeOut()
    {
        UI_Alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
