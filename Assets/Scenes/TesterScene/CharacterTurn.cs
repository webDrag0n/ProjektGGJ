using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterTurn : MonoBehaviour
{
    private bool facingRight;
    private Vector3 scale;
    
    void Flip(bool isRight) {
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        if (isRight!=facingRight)
        {
            scale.x *= -1;
            transform.localScale = scale;
            facingRight = !facingRight;
        }
    }

    private void Start()
    {
        scale = transform.localScale;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 hingePosition = transform.position;
        Flip((mousePosition.x - hingePosition.x)<0);
    }
    
}
