using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public int healAmount;
    public void HealPlayer()
    {
        PlayerStats.instance.TakeDamage(-healAmount);
    }
}
