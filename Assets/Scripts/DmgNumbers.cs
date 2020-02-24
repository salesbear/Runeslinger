using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgNumbers : MonoBehaviour
{

    [SerializeField] float hiRollAngle = 0f;
    [SerializeField] float loRollAngle = 50f; //in degrees
    [SerializeField] float hiRollMagn = 1f;
    [SerializeField] float loRollMagn = 1.5f;
    public float rolledAngle { get; private set; }
    public float rolledMagnitude { get; private set; }
    public TextMeshProUGUI dmgText { get; private set; }
    public Color noDamageColor { get; private set; } = new Color32(150, 150, 150, 255);
    public Color shieldDamageColor { get; private set; } = new Color32(108, 192, 221, 255);

    void OnEnable()
    {
        rolledAngle = Random.Range(loRollAngle, hiRollAngle);
        rolledMagnitude = Random.Range(loRollMagn, hiRollMagn);
    }
    
    void Awake()
    {
        dmgText = GetComponent<TextMeshProUGUI>();
    }

    //replaces the text in the prefab with a number or whatever you want, really
    //Default text is "0"
    //Default Color is Red
    void WriteDamage(int number)
    {
        dmgText.text = number.ToString();
    }
    //overload to change the color of the number (for no damage or blocked damage);
    void WriteDamage(int number, Color color)
    {
        dmgText.text = number.ToString();
        dmgText.color = color;
    }
}
