using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShieldEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    Color32 colorStart;
    Color32 colorEnd;
    public byte startAlpha;
    public byte endAlpha;
    public float duration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        colorStart = new Color32(255, 255, 255, startAlpha);
        spriteRenderer.color = colorStart;

        colorEnd = new Color32(255, 255, 255, endAlpha);

        StartCoroutine(Shield());
    }

    IEnumerator Shield()
    {
        // raise alpha to full
        spriteRenderer.DOColor(colorEnd, duration);

        yield return new WaitForSeconds(duration);

        // lower alpha back to start
        spriteRenderer.DOColor(colorStart, duration);
    }
}
