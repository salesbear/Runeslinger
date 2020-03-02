using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSound : MonoBehaviour
{
    public void FindButtonSound()
    {
        DelayedStop sound = DelayedStop.FindObjectOfType<DelayedStop>();
        sound.Restart();
    }
}
