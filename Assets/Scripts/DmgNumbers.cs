using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgNumbers : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float hiRollAngle = 0f;
    [SerializeField] float loRollAngle = 50f; //in degrees
    [SerializeField] float hiRollMagn = 1f;
    [SerializeField] float loRollMagn = 1.5f;
    public float rolledAngle { get; private set; }
    public float rolledMagnitude { get; private set; }
    public TextMeshProUGUI dmgText { get; private set; }
    public static Color damageColor { get; private set; } = new Color32(255, 0, 0, 255);
    public static Color noDamageColor { get; private set; } = new Color32(100, 100, 100, 255);
    public static Color shieldDamageColor { get; private set; } = new Color32(98, 182, 211, 255);

    void OnEnable()
    {
        rolledAngle = Random.Range(loRollAngle, hiRollAngle);
        rolledMagnitude = Random.Range(loRollMagn, hiRollMagn);
    }

    void Awake()
    {
        dmgText = GetComponentInChildren<TextMeshProUGUI>();
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    //replaces the text in the prefab with a number or whatever you want, really
    //changes the color of the text to whatever you want, three colors are provided above.
    //Default text is "0"
    //Default Color is Red
    public void WriteDamage(int number, Color color)
    {
        dmgText.text = number.ToString();
        dmgText.color = color;
    }

    public void Launch()
    {
        Vector2 gamingVector = AngleToVector2(rolledAngle, rolledMagnitude);
        rb.velocity = gamingVector;
    }

    private Vector2 AngleToVector2(float angle, float magnitude)
    {
        return (new Vector2(Mathf.Cos(Mathf.Deg2Rad*angle), Mathf.Sin(Mathf.Deg2Rad*angle)) * magnitude);
    }

}
