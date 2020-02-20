using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleEye : CardBehavior
{
    public override void PlayCard()
    {
        base.PlayCard();
        PlayerStats.instance.CallStatus(2, 1, -1);
    }
}
