﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : CardBehavior
{
    public override void PlayCard()
    {
        PlayerStats.instance.CallStatus(2, PlayerStats.instance.posStatus, 2, 1);
        base.PlayCard();
    }
}
