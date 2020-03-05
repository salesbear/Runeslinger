using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI grit;
    [SerializeField] GameObject accuracyIndicator;
    [SerializeField] TextMeshProUGUI accuracy;
    [SerializeField] GameObject shieldBar;
    [SerializeField] TextMeshProUGUI shield;

    public float enlargedScale;
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
            health.text = PlayerStats.instance.playerClass.currentHealth.ToString() + "/" + PlayerStats.instance.playerClass.maxHealth.ToString();
            healthBar.maxValue = PlayerStats.instance.playerClass.maxHealth;
            healthBar.value = PlayerStats.instance.playerClass.currentHealth;
            grit.text = PlayerStats.instance.playerClass.currentGrit.ToString() + "/" + PlayerStats.instance.playerClass.maxGrit.ToString();
            if (PlayerStats.instance.playerClass.accuracy == 0)
            {
                accuracyIndicator.SetActive(false);
            }
            else
            {
                accuracyIndicator.SetActive(true);
                accuracy.text = PlayerStats.instance.playerClass.accuracy.ToString();
            }
            if (PlayerStats.instance.playerClass.shield == 0)
            {
                shieldBar.SetActive(false);
            }
            else
            {
                shieldBar.SetActive(true);
                shield.text = PlayerStats.instance.playerClass.shield.ToString();
            }
        }
    }

    public void PopHealthText()
    {
        /*
        RectTransform t = health.GetComponent<RectTransform>();
        t.DOScaleX(enlargedScale, duration);
        t.DOScaleY(enlargedScale, duration);
        StartCoroutine(ShrinkText(t, duration));
        */
    }

    public void PopGritText()
    {
        RectTransform t = grit.GetComponent<RectTransform>();
        Vector3 originalScale = t.localScale;
        t.DOScaleX(enlargedScale, duration);
        t.DOScaleY(enlargedScale, duration);
        StartCoroutine(ShrinkText(t, duration, originalScale));
    }

    public void PopAccuracyText()
    {
        RectTransform t = accuracy.GetComponent<RectTransform>();
        Vector3 originalScale = t.localScale;
        t.DOScaleX(enlargedScale, duration);
        t.DOScaleY(enlargedScale, duration);
        StartCoroutine(ShrinkText(t, duration, originalScale));
    }

    public void PopShieldText()
    {
        /*
        RectTransform t = shield.GetComponent<RectTransform>();
        t.DOScaleX(enlargedScale, duration);
        t.DOScaleY(enlargedScale, duration);
        StartCoroutine(ShrinkText(t, duration));
        */
    }

    IEnumerator ShrinkText(RectTransform t, float duration, Vector3 scale)
    {
        yield return new WaitForSeconds(duration);
        t.DOScaleX(scale.x, duration);
        t.DOScaleY(scale.y, duration);
    }
}
