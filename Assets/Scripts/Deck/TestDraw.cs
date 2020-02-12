using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour
{
    [SerializeField] Stack stack;
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            stack.Draw();
        }
    }
}
