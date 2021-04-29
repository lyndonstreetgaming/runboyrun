using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Fader : MonoBehaviour
{
    public Image image;

    public AnimationCurve animationCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;

            float a = animationCurve.Evaluate(t);

            image.color = new Color(0f, 0f, 0f, t);

            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;

            float a = animationCurve.Evaluate(t);

            image.color = new Color(0f, 0f, 0f, t);

            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}