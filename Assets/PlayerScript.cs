using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int shield;
    [SerializeField] Stack deck;
    public GameObject card;
    public GameObject target;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) == true)
        {
            deck.Draw();
            Debug.Log("You aren't crazy");
        }
    }
}
