using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLinkedSounds : MonoBehaviour
{
    [SerializeField] GameObject buttonSound;
    // Start is called before the first frame update
    public void ButtonSound()
    {
        Instantiate(buttonSound);
        buttonSound.transform.SetParent(gameObject.transform);
    }
}
