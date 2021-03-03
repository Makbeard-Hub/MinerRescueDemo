using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fader : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [SerializeField] float fadeOutTime = 1;
    [SerializeField] float fadedWaitTime = 1;
    [SerializeField] float fadeInTime = 1;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator FadeOut(float fadeTime)
    {
        canvasGroup.blocksRaycasts = true;

        while (canvasGroup.alpha < 1) //alpha is not 1
        {
            // moving alpha towards 1
            canvasGroup.alpha += Time.deltaTime / fadeTime;

            yield return null; //waits for 1 frame
        }
    }

    public IEnumerator FadedWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator FadeIn(float fadeTime)
    {

        while (canvasGroup.alpha > 0) //alpha is not 0
        {
            // moving alpha towards 0
            canvasGroup.alpha -= Time.deltaTime / fadeTime;

            yield return null; //waits for 1 frame
        }
        canvasGroup.blocksRaycasts = false;
    }

    public void FadeOutImmediate()
    {
        canvasGroup.blocksRaycasts = true;

        canvasGroup.alpha = 1;
    }
}