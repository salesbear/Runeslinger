using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePitch : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    [SerializeField] float minRange = 0.0f;
    [SerializeField] float maxRange = 3.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        sound.pitch = Random.Range(minRange, maxRange);
    }
    void Start()
    {
        sound.Play();
    }
}
