using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WinLoseText : MonoBehaviour
{
    RectTransform winLoseText;
    public float scale;
    public float duration;

    private void OnEnable()
    {
        winLoseText = GetComponent<RectTransform>();
        winLoseText.DOScale(scale, duration);
    }
}
