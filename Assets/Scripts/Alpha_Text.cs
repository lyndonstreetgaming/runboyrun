using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alpha_Text : MonoBehaviour
{
    public float FadeSpeed;

    private float Count;

    public TMP_Text TextComponent;

    private void Start()
    {
        TextComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        Count += FadeSpeed * Time.deltaTime;

        TextComponent.color = new Color(1f, 1f, 1f, Mathf.Sin(Count) * 1f);

    }
}
