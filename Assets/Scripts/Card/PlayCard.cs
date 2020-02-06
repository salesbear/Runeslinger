using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCard : MonoBehaviour
{
    //the card we're playing
    private Card card;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(int damage, IDamagable target)
    {
        target.TakeDamage(damage);
    }
}
