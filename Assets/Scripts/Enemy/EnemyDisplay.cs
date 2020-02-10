using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    public Enemy enemy;

    [Header("Game Stuff")]
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyHP;
    public Slider enemyHPBar;
    public TextMeshProUGUI rolledDMG;


    [Header("Image Stuff")]
    public Image enemyImage;
    public Image enemyBehavior;

    [Header("Behavior Icons")]
    public Sprite doNothing;
    public Sprite attack;
    public Sprite defend;
    public Sprite buff;
    public Sprite debuff;
    public Sprite other;

    private void OnEnable()
    {
        enemy.EnemyUIUpdate += UpdateUI;
        enemy.Die += OnDie;
    }

    private void OnDisable()
    {
        enemy.EnemyUIUpdate -= UpdateUI;
        enemy.Die -= OnDie;
    }

    void Start()
    {
        enemy.SetUp(); 
    }

    private void UpdateUI()
    {
        ReadEnemyFromAsset();
    }
    
    void OnDie()
    {
        enemy.currentHP = 0;
        ReadEnemyFromAsset();

        this.gameObject.SetActive(false);
    }

    void ReadEnemyFromAsset()
    {
        if (enemy != null)
        {
            enemyName.text = enemy.enemyName;
            enemyHP.text = enemy.currentHP + "/" + enemy.maxHP;
            enemyHPBar.maxValue = enemy.maxHP;
            enemyHPBar.minValue = 0;
            enemyHPBar.value = enemy.currentHP;
            enemyImage.sprite = enemy.enemySprite;

            //some hot bullshit incoming
            if(enemy.preparedAction == PreparedAction.DoNothing)
            {
                enemyBehavior.sprite = doNothing;
                rolledDMG.gameObject.SetActive(false);
            }
            else if (enemy.preparedAction == PreparedAction.Attack)
            {
                enemyBehavior.sprite = attack;
                rolledDMG.gameObject.SetActive(true);
                rolledDMG.text = enemy.rolledDamage.ToString();
            }
            else if (enemy.preparedAction == PreparedAction.Defend)
            {
                enemyBehavior.sprite = defend;
                rolledDMG.gameObject.SetActive(false);
            }
            else if (enemy.preparedAction == PreparedAction.Buff)
            {
                enemyBehavior.sprite = buff;
                rolledDMG.gameObject.SetActive(false);
            }
            else if (enemy.preparedAction == PreparedAction.Debuff)
            {
                enemyBehavior.sprite = debuff;
                rolledDMG.gameObject.SetActive(false);
            }
            else if (enemy.preparedAction == PreparedAction.Other)
            {
                enemyBehavior.sprite = other;
                rolledDMG.gameObject.SetActive(false);
            }
        }
    }
}

