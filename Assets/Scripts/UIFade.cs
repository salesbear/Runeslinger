﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIFade : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private Color colorStart;
    public Color colorEnd;
    [SerializeField] float duration;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorStart = spriteRenderer.color;
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.DOColor(colorEnd, duration);
        StartCoroutine(FadeOut(duration));
    }

    IEnumerator FadeOut(float duration)
    {
        yield return new WaitForSeconds(duration);
        spriteRenderer.DOColor(colorStart, duration);
    }
}
