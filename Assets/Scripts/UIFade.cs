using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIFade : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Color colorStart;
    public Color colorEnd;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.DOColor(colorEnd, duration);
        StartCoroutine(FadeOut(duration));
    }

    IEnumerator FadeOut(float duration)
    {
        yield return new WaitForSeconds(duration);
        spriteRenderer.DOColor(colorStart, duration);
    }
}
