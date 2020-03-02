using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStop : MonoBehaviour
{
    [SerializeField] int stopTime;
    int timer;
    [SerializeField] AudioSource self;
    [SerializeField] float minRange = 0.0f;
    [SerializeField] float maxRange = 3.0f;
    void Update()
    {
        if (timer == 0)
        {
            self.Stop();
        }
        else if (timer >= 0)
        {
            timer -= 1;
        }
    }

    public void Restart()
    {
        self.pitch = Random.Range(minRange, maxRange);
        timer = stopTime;
        self.Play();
    }
}