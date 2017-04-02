using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimateText : MonoBehaviour
{
    private float timeElapsed = 0;
    private bool fadeInComplete = false;
    private bool fadeOutComplete = false;

    public float FadeInTime;
    public float FadeOutTime;

    public void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > FadeInTime && !fadeInComplete)
        {
            FadeIn();
            fadeInComplete = true;
        }
        if(timeElapsed > FadeOutTime && !fadeOutComplete)
        {
            FadeOut();
            fadeOutComplete = true;
        }

    }

    public void FadeIn()
    {
        StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
    }

    public IEnumerator FadeTextToFullAlpha(float time, Text textToAnimate)
    {
        textToAnimate.color = new Color(textToAnimate.color.r, textToAnimate.color.g, textToAnimate.color.b, 0);
        while (textToAnimate.color.a < 1.0f)
        {
            textToAnimate.color = new Color(textToAnimate.color.r, textToAnimate.color.g, textToAnimate.color.b, textToAnimate.color.a + (Time.deltaTime / time));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float time, Text textToAnimate)
    {
        textToAnimate.color = new Color(textToAnimate.color.r, textToAnimate.color.g, textToAnimate.color.b, 1);
        while (textToAnimate.color.a > 0.0f)
        {
            textToAnimate.color = new Color(textToAnimate.color.r, textToAnimate.color.g, textToAnimate.color.b, textToAnimate.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }
}