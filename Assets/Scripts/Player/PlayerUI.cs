using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI grit;
    [SerializeField] TextMeshProUGUI accuracy;
    [SerializeField] TextMeshProUGUI shield;

    public float enlargeAmt;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.instance != null)
        {
            health.text = "Health: " + PlayerStats.instance.playerClass.currentHealth;
            grit.text = "Grit: " + PlayerStats.instance.playerClass.currentGrit;
            accuracy.text = "Acc: " + PlayerStats.instance.playerClass.accuracy;
            shield.text = "Shield: " + PlayerStats.instance.playerClass.shield;
        }
    }

    public void PopHealthText()
    {
        RectTransform t = health.GetComponent<RectTransform>();
        t.DOScaleX(t.localScale.x + enlargeAmt, duration);
        t.DOScaleY(t.localScale.y + enlargeAmt, duration);
        StartCoroutine(ShrinkText(t, duration));
    }

    public void PopGritText()
    {
        RectTransform t = grit.GetComponent<RectTransform>();
        t.DOScaleX(t.localScale.x + enlargeAmt, duration);
        t.DOScaleY(t.localScale.y + enlargeAmt, duration);
        StartCoroutine(ShrinkText(t, duration));
    }

    public void PopAccuracyText()
    {
        RectTransform t = accuracy.GetComponent<RectTransform>();
        t.DOScaleX(t.localScale.x + enlargeAmt, duration);
        t.DOScaleY(t.localScale.y + enlargeAmt, duration);
        StartCoroutine(ShrinkText(t, duration));
    }

    public void PopShieldText()
    {
        RectTransform t = shield.GetComponent<RectTransform>();
        t.DOScaleX(t.localScale.x + enlargeAmt, duration);
        t.DOScaleY(t.localScale.y + enlargeAmt, duration);
        StartCoroutine(ShrinkText(t, duration));
    }

    IEnumerator ShrinkText(RectTransform t, float duration)
    {
        yield return new WaitForSeconds(duration);
        t.DOScaleX(t.localScale.x - enlargeAmt, duration);
        t.DOScaleY(t.localScale.y - enlargeAmt, duration);
    }
}
