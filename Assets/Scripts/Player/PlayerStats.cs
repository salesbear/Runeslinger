using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    public BaseClassless playerClass;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = new BaseClassless();
    }
}
