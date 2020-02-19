using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCard : MonoBehaviour
{
    public float enlargeX = 25;
    public float enlargeY = 25;

    public void OnMouseEnter()
    {
        transform.localScale += new Vector3(enlargeX, enlargeY, 1);
    }


    public void OnMouseExit()
    {
        // assuming you want it to return to its original size when your mouse leaves it.
        transform.localScale = new Vector3(50, 50, 1); 
    }
}
