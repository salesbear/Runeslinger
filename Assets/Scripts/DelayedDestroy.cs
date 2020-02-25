using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    [SerializeField] int destroy;
    void Update()
    {
        if (destroy == 0)
        {
            Destroy(gameObject);
        }
        destroy -= 1;
    }
}
