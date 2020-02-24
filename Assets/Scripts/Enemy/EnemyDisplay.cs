using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDisplay : MonoBehaviour, IDamagable
{
    private EnemyController enemyController;
    public Enemy enemy;
    //the rect transform for the enemy, used for some card stuff
    [ReadOnly]
    public RectTransform enemyTransform;
    [Header("Game Stuff")]
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyHP;
    public TextMeshProUGUI shieldValue;
    public Slider enemyHPBar;
    public Slider enemyShield;
    public TextMeshProUGUI rolledDMG;
    GameObject damageNumber = Resources.Load<GameObject>("DamageNumbers");


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
        enemy.DamageNumber += OnDamage;
    }

    private void OnDisable()
    {
        enemy.EnemyUIUpdate -= UpdateUI;
        enemy.Die -= OnDie;
        enemy.DamageNumber -= OnDamage;
    }
    //register with enemy controller on awake
    void Awake()
    {
        enemyController = FindObjectOfType<EnemyController>();
        enemyController.AddEnemy(this);
        enemyTransform = GetComponentInChildren<RectTransform>();
    }
    //use this to initialize variables
    void Start()
    {
        Debug.Log(enemy.enemyName);
        enemy.SetUp();
        UpdateUI();
    }

    private void UpdateUI()
    {
        ReadEnemyFromAsset();
    }
    
    void OnDie()
    {
        enemy.currentHP = 0;
        ReadEnemyFromAsset();
        //take self off enemy controller list on death
        enemyController.RemoveEnemy(this);
        if (this != null)
        {
            this.gameObject.SetActive(false);
        }
    }

    //I suspect the bugs we were getting here were coming from stuff loading in at different times, which is annoying
    //but checking for if the thing is null seems to have fixed the bug, for some reason
    void OnDamage(int damage, Color color)
    {
        GameObject dmg = Instantiate(damageNumber, gameObject.transform.position, Quaternion.identity);
        DmgNumbers dmgNum = dmg.GetComponent<DmgNumbers>();
        dmgNum.WriteDamage(damage, color);
        dmgNum.Launch();
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
            if (enemyShield != null)
            {
                if(enemy.currentShield != 0)
                {
                    enemyShield.gameObject.SetActive(true);
                    shieldValue.text = enemy.currentShield.ToString();
                }
                else
                {
                    enemyShield.gameObject.SetActive(false);
                    shieldValue.text = enemy.currentShield.ToString();
                }
            }
            else
            {
                Debug.Log("The shield is null");
            }

            if (enemy == null)
            {
                Debug.Log("enemy is null");
            }

            enemyImage.sprite = enemy.enemySprite;

            //some hot bullshit incoming
            if(enemy.preparedAction == PreparedAction.DoNothing)
            {
                enemyBehavior.sprite = doNothing;
                if (rolledDMG != null)
                {
                    rolledDMG.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("rolledDMG is null");
                }
            }
            else if (enemy.preparedAction == PreparedAction.Attack)
            {
                enemyBehavior.sprite = attack;
                if (rolledDMG != null)
                {
                    rolledDMG.gameObject.SetActive(true);
                    rolledDMG.text = enemy.rolledDamage.ToString();
                }
                else
                {
                    Debug.Log("rolledDMG is null");
                }
            }
            else if (enemy.preparedAction == PreparedAction.Defend)
            {
                enemyBehavior.sprite = defend;
                if (rolledDMG != null)
                {
                    rolledDMG.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("RolledDMG is null");
                }
            }
            else if (enemy.preparedAction == PreparedAction.Buff)
            {
                enemyBehavior.sprite = buff;
                if (rolledDMG != null)
                {
                    rolledDMG.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("RolledDMG is null");
                }
            }
            else if (enemy.preparedAction == PreparedAction.Debuff)
            {
                enemyBehavior.sprite = debuff;
                if (rolledDMG != null)
                {
                    rolledDMG.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("RolledDMG is null");
                }
            }
            else if (enemy.preparedAction == PreparedAction.Other)
            {
                enemyBehavior.sprite = other;
                if (rolledDMG != null)
                {
                    rolledDMG.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("RolledDMG is null");
                }
            }
        }
    }

    public void TakeDamage(int damageTaken)
    {
        enemy.TakeDamage(damageTaken);
    }
}

